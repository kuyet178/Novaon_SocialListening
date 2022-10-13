using System.Diagnostics;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;

namespace AioCore.Services.AutomationServices;

public interface IADBService
{
    Task<string> ExecuteAsync(string deviceId, string command);

    Task<bool> InstalledAsync(string deviceId, string packageName);

    Task SetPortraitAsync(string deviceId);

    Task OpenAppAsync(string deviceId, string packageName);

    Task ClearAppDataAsync(string deviceId, string packageName);
    
    Task CloseAppAsync(string deviceId, string packageName);

    Task InstallAppAsync(string deviceId, string packagePath);

    Task ToastAsync(string deviceId, string message);
}

public class ADBService : IADBService
{
    private readonly IWebHostEnvironment _environment;

    public ADBService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task<string> ExecuteAsync(string deviceId, string command)
    {
        if (deviceId.Contains(':') && command.Contains(deviceId))
        {
            command = command.Replace(deviceId, NormalizeDeviceName(deviceId));
        }

        using var process = new Process();
        command = string.Concat(new string[4] { " -s ", deviceId, " ", command });
        var assemblyPath = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location);
        var fileName = assemblyPath + "\\Assemblies\\adb.exe";
        process.StartInfo = new ProcessStartInfo
        {
            FileName = fileName,
            Arguments = command,
            UseShellExecute = false,
            WindowStyle = ProcessWindowStyle.Hidden,
            CreateNoWindow = true,
            RedirectStandardError = true,
            RedirectStandardOutput = true
        };
        process.Start();
        var standardOutput = process.StandardOutput;
        var text = await standardOutput.ReadToEndAsync();
        await process.WaitForExitAsync();
        return text.Trim();
    }

    public async Task<bool> InstalledAsync(string deviceId, string packageName)
    {
        var result = false;
        var num = 0;
        while (num < 3)
        {
            var response = await ExecuteAsync(deviceId, "shell \"pm list packages\"");
            if (!response.Contains(packageName))
            {
                num++;
                Thread.Sleep(500);
                continue;
            }

            result = true;
            break;
        }

        return result;
    }

    public async Task OpenAppAsync(string deviceId, string packageName)
    {
        await ExecuteAsync(deviceId, $"shell monkey -p '{packageName}' -c android.intent.category.LAUNCHER 1");
    }

    public async Task ClearAppDataAsync(string deviceId, string packageName)
    {
        await ExecuteAsync(deviceId, $"shell pm clear {packageName}");
    }

    public async Task CloseAppAsync(string deviceId, string packageName)
    {
        await ExecuteAsync(deviceId, $"shell am force-stop {packageName}");
    }

    public async Task InstallAppAsync(string deviceId, string packagePath)
    {
        await ExecuteAsync(deviceId, " install \"" + packagePath + "\"");
    }

    public async Task ToastAsync(string deviceId, string message)
    {
        await ExecuteAsync(deviceId, $"shell am start -n \"com.aioc.farm/.ToastActivity\"  --es sms '{message}'");
    }

    public async Task SetPortraitAsync(string deviceId)
    {
        await ExecuteAsync(deviceId,
            " shell content insert --uri content://settings/system --bind name:s:user_rotation --bind value:i:0");
        await ExecuteAsync(deviceId, "shell settings put system accelerometer_rotation 0");
    }

    private static string NormalizeDeviceName(string serialNo)
    {
        return serialNo.Replace(".", "").Replace(":", "");
    }
}