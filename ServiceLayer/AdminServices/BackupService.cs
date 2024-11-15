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
    Task CreateBackup(string? targetDirectoryPath);
}
public class BackupService(IConfiguration configuration) : IBackupService
{
    public async Task CreateBackup(string? targetDirectoryPath)
    {
#warning validate targetDirectory
        var backupInfo = configuration.GetRequiredSection("BackupData");
        var userCredentials = backupInfo.GetRequiredSection("UserCredentials");
        string userName = (string)userCredentials.GetValue(typeof(string), "Name")!;
        string password = (string)userCredentials.GetValue(typeof(string), "Password")!;
        string hostName = (string)backupInfo.GetValue(typeof(string), "Host")!;
        string dbName = (string)backupInfo.GetValue(typeof(string), "Database")!;
        if (string.IsNullOrWhiteSpace(targetDirectoryPath))
            targetDirectoryPath = (string)backupInfo.GetValue(typeof(string), "DefaultBackupPath")!;
        EnsureDirectory(targetDirectoryPath);
        string fileName = $"{DateTimeOffset.UtcNow.ToString(ConstValues.BackupDateTimeFormat)} backup.sql";

        ProcessStartInfo processInfo = new("mysqldump", $"-u{userName} -p{password} -h {hostName} --databases {dbName} --result-file=\"{Path.Combine(targetDirectoryPath, fileName)}\"")
        {
            UseShellExecute = false,
            RedirectStandardError = true,
            RedirectStandardOutput = true,
        };

        //process.StartInfo.FileName = ;
        //process.StartInfo.Arguments = ;
        var process = Process.Start(processInfo);
        //process.StandardInput.WriteLine("mysqldump -uroot -ppassword -h localhost musical_shop > C:/sql/backups/musical_shop.sql");
        Debug.WriteLine(process!.StandardOutput.ReadToEnd());
        Debug.WriteLine(process!.StandardError.ReadToEnd());
        //process!.OutputDataReceived += (sender, args) => Debug.WriteLine(args.Data);
        await process!.WaitForExitAsync();
    }
    public void EnsureDirectory(string directoryPath)
    {
        DirectoryInfo directoryInfo = new(directoryPath);
        if (!directoryInfo.Exists)
            directoryInfo.Create();
    }
}
