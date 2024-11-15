using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.AdminServices;
public interface IBackupService
{
    Task<string> CreateBackup(string note);
    Task<Dictionary<DateTime, string>> GetBackups();
}
public class BackupService(IConfiguration configuration) : IBackupService
{
#warning validate note length and its characters like :?*<>.,"
    public async Task<string> CreateBackup(string note)
    {
        var backupInfo = configuration.GetRequiredSection("BackupData");
        var userCredentials = backupInfo.GetRequiredSection("UserCredentials");
        string userName = (string)userCredentials.GetValue(typeof(string), "Name")!;
        string password = (string)userCredentials.GetValue(typeof(string), "Password")!;
        string hostName = (string)backupInfo.GetValue(typeof(string), "Host")!;
        string dbName = (string)backupInfo.GetValue(typeof(string), "Database")!;
        string directory = (string)backupInfo.GetValue(typeof(string), "DefaultDirectory")!;
        EnsureDirectory(directory);
        string fileName = $"{DateTimeOffset.UtcNow.ToString(ConstValues.BackupDateTimeFormat)}{note}.sql";
        string fullFileName = Path.Combine(directory, fileName);
        ProcessStartInfo processInfo = new("mysqldump", $"-u{userName} -p{password} -h {hostName} --databases {dbName} --result-file=\"{fullFileName}\"")
        {
            UseShellExecute = false,
            //RedirectStandardError = true,
            //RedirectStandardOutput = true
        };
        await Process.Start(processInfo)!
                     .WaitForExitAsync();
        //Debug.WriteLine(process!.StandardOutput.ReadToEnd());
        //Debug.WriteLine(process!.StandardError.ReadToEnd());
        return fullFileName;
    }
    private void EnsureDirectory(string directoryPath)
    {
        DirectoryInfo directoryInfo = new(directoryPath);
        if (!directoryInfo.Exists)
            directoryInfo.Create();
    }
    public async Task<Dictionary<DateTime, string>> GetBackups()
    {

    }
}
