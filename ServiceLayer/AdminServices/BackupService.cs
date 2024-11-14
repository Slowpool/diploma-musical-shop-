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
    Task CreateBackup(string? filePath);
}
public class BackupService(IConfiguration configuration) : IBackupService
{
    public async Task CreateBackup(string? filePath)
    {
        var backupInfo = configuration.GetRequiredSection("BackupData");
        var userCredentials = backupInfo.GetRequiredSection("UserCredentials");
        string userName = (string)userCredentials.GetValue(typeof(string), "Name")!;
        string password = (string)userCredentials.GetValue(typeof(string), "Password")!;
        string hostName = (string)backupInfo.GetValue(typeof(string), "Host")!;
        string dbName = (string)backupInfo.GetValue(typeof(string), "Database")!;
        if (string.IsNullOrWhiteSpace(filePath))
            filePath = (string)backupInfo.GetValue(typeof(string), "DefaultBackupPath")!;
        using Process process = new();
        process.StartInfo.FileName = "mysqldump";
        process.StartInfo.Arguments = $"-u{userName} -p{password} -h {hostName} {dbName} > {filePath}";
        process.Start();
        await process.WaitForExitAsync();
        //process.StartInfo.CreateNoWindow = false;
        //process.StartInfo.

    }
}
