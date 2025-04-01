using BizLogicBase.Validation;
using Humanizer.Configuration;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.AdminServices;
public interface IBackupService : IErrorAdder
{
    Task<string> CreateBackup(string note);
    Task<Dictionary<DateTime, string>> GetBackups();
    Task ApplyRestoreFromBackup(DateTime backupDateTime);
}

public class BackupService : ErrorAdder, IBackupService
{
    private readonly IConfigurationSection _backup;
    private readonly string _dirName;
    private readonly DirectoryInfo _dirInfo;
    private IConfigurationSection UserCredentials => _backup.GetRequiredSection("UserCredentials");
    private string UserName => (string)UserCredentials.GetValue(typeof(string), "Name")!;
    private string UserPassword => (string)UserCredentials.GetValue(typeof(string), "Password")!;
    private string DbName => (string)_backup.GetValue(typeof(string), "Database")!;

    public BackupService(IConfiguration configuration)
    {
        _backup = configuration.GetRequiredSection("BackupData");
        var appRootDir = new DirectoryInfo(Directory.GetCurrentDirectory())!.Parent!;
        var relatedPath = (string)_backup.GetValue(typeof(string), "RelatedPath")!;
        _dirName = Path.Combine(appRootDir.FullName, relatedPath);
        _dirInfo = new DirectoryInfo(_dirName);
    }

#warning validate note length and its characters like :?*<>.,"
    public async Task<string> CreateBackup(string note)
    {
        string hostName = (string)_backup.GetValue(typeof(string), "Host")!;
        EnsureDirectory();
        string fileName = $"{DateTimeOffset.UtcNow.ToString(Consts.BackupDateTimeFormat)}{note}.sql";
        string fullFileName = Path.Combine(_dirName, fileName);
        ProcessStartInfo processInfo = new("mysqldump", string.Format(Cmd.MysqldumpArguments, UserName, UserPassword, hostName, DbName, fullFileName))
        {
            UseShellExecute = false,
            //RedirectStandardError = true,
            //RedirectStandardOutput = true
        };
        var process = Process.Start(processInfo)!;
        //Debug.WriteLine(process!.StandardOutput.ReadToEnd());
        //Debug.WriteLine(process!.StandardError.ReadToEnd());
        await process.WaitForExitAsync();
        return fullFileName;
    }

    private void EnsureDirectory()
    {
        if (!_dirInfo.Exists)
            _dirInfo.Create();
    }

    public async Task<Dictionary<DateTime, string>> GetBackups()
    {
        if (!_dirInfo.Exists)
            return [];
        Dictionary<DateTime, string> result = [];
        DateTime dateTime;
        const string format = Consts.BackupDateTimeFormat;
        foreach (var fileInfo in _dirInfo.GetFiles())
        {
            dateTime = DateTime.ParseExact(fileInfo.Name.AsSpan(0, format.Length), format, null);
            result[dateTime] = fileInfo.Name.AsSpan(format.Length, fileInfo.Name.Length - ".sql".Length - format.Length).ToString();
        }
        return result;
    }

    public async Task ApplyRestoreFromBackup(DateTime backupDateTime)
    {
        // checking
        if (!_dirInfo.Exists)
        {
            AddError("Резервные копии не найдены.");
            return;
        }
        string formattedDateTime = backupDateTime.ToString(Consts.BackupDateTimeFormat);
        var file = _dirInfo.GetFiles()
                                        .Where(file => file.Name.StartsWith(formattedDateTime))
                                        .SingleOrDefault();
        if(file == null)
        {
            AddError("Резервная копия с данной датой не найдена");
            return;
        }
#warning ask user permission to database dropping somewhere here
        string commandToExecute = string.Format(SqlStatements.RestoreDatabaseFromBackup, DbName, file.FullName.Replace('\\', '/'));
        ProcessStartInfo processStartInfo = new()
        {
            FileName = "mysql",
            Arguments = string.Format(Cmd.MysqRestoreBackupArgs, UserName, UserPassword, commandToExecute),
            UseShellExecute = false,
            RedirectStandardOutput = true,
        };
        var process = Process.Start(processStartInfo)!;
        Debug.WriteLine(process.StandardOutput.ReadToEnd());
        await process.WaitForExitAsync();
    }
}
