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
    private readonly IConfigurationSection _backupInfo;
    private readonly string _backupsDirectoryName;
    private readonly DirectoryInfo _backupsDirectoryInfo;
    private IConfigurationSection UserCredentials => _backupInfo.GetRequiredSection("UserCredentials");
    private string UserName => (string)UserCredentials.GetValue(typeof(string), "Name")!;
    private string UserPassword => (string)UserCredentials.GetValue(typeof(string), "Password")!;
    private string DbName => (string)_backupInfo.GetValue(typeof(string), "Database")!;

    public BackupService(IConfiguration configuration)
    {
        _backupInfo = configuration.GetRequiredSection("BackupData");
        _backupsDirectoryName = (string)_backupInfo.GetValue(typeof(string), "Directory")!;
        _backupsDirectoryInfo = new DirectoryInfo(_backupsDirectoryName);

    }

#warning validate note length and its characters like :?*<>.,"
    public async Task<string> CreateBackup(string note)
    {
        string hostName = (string)_backupInfo.GetValue(typeof(string), "Host")!;
        EnsureDirectory();
        string fileName = $"{DateTimeOffset.UtcNow.ToString(ConstValues.BackupDateTimeFormat)}{note}.sql";
        string fullFileName = Path.Combine(_backupsDirectoryName, fileName);
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
        if (!_backupsDirectoryInfo.Exists)
            _backupsDirectoryInfo.Create();
    }

    public async Task<Dictionary<DateTime, string>> GetBackups()
    {
        if (!_backupsDirectoryInfo.Exists)
            return [];
        Dictionary<DateTime, string> result = [];
        DateTime dateTime;
        const string format = ConstValues.BackupDateTimeFormat;
        foreach (var fileInfo in _backupsDirectoryInfo.GetFiles())
        {
            dateTime = DateTime.ParseExact(fileInfo.Name.AsSpan(0, format.Length), format, null);
            result[dateTime] = fileInfo.Name.AsSpan(format.Length, fileInfo.Name.Length - ".sql".Length - format.Length).ToString();
        }
        return result;
    }

    public async Task ApplyRestoreFromBackup(DateTime backupDateTime)
    {
        // checking
        if (!_backupsDirectoryInfo.Exists)
        {
            AddError("Резервные копии не найдены.");
            return;
        }
        string formattedDateTime = backupDateTime.ToString(ConstValues.BackupDateTimeFormat);
        var file = _backupsDirectoryInfo.GetFiles()
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
