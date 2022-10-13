using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AioCore.Services.CommonServices;

public interface ICommandService
{
    Task<string> ExecuteAsync(string command);

    Task<List<string>> NetstatListening();

    Task<bool> KillProcessAsync(int port);
}

public class CommandService : ICommandService
{
    public async Task<string> ExecuteAsync(string command)
    {
        try
        {
            var rootPath = Environment.GetEnvironmentVariable("SystemRoot");
            using var process = new Process();
            process.StartInfo = new ProcessStartInfo
            {
                FileName = rootPath + "\\System32\\cmd.exe",
                Arguments = "/c " + command,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                RedirectStandardError = true
            };
            process.Start();
            var standardOutput = process.StandardOutput;
            var text = await standardOutput.ReadToEndAsync();
            await process.WaitForExitAsync();
            return text.Trim();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<string>> NetstatListening()
    {
        var result = new List<string>();
        const string command = "netstat -an |find /i \"listening\"";
        var response = await ExecuteAsync(command);
        var items = response.Split('\n').Select(x => x.Trim());
        foreach (var item in items)
        {
            if (!item.Contains("127.0.0") && !item.Contains("0.0.0.0"))
                continue;

            var matchCollection = Regex.Matches(item, "(.*?)\\:(.*?) ", RegexOptions.Singleline);
            if (matchCollection.Count <= 0) continue;
            var subItem = matchCollection[0].Groups[2].Value.Trim();
            if (int.TryParse(subItem, out _))
            {
                result.Add(subItem);
            }
        }

        return result;
    }

    public async Task<bool> KillProcessAsync(int port)
    {
        try
        {
            var processIdByPort = await GetProcessIdByPort(port);
            foreach (var processById in processIdByPort.Select(Process.GetProcessById))
            {
                processById.Kill();
            }

            return true;
        }
        catch (ArgumentException)
        {
            return false;
        }
    }

    private async Task<List<int>> GetProcessIdByPort(int port)
    {
        var list = new List<int>();
        var response = await ExecuteAsync($"netstat -a -n -o | find \"{port}\"");
        if (response == "") return list.Distinct().ToList();
        var array = response.Trim().Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        foreach (var text2 in array)
        {
            var array3 = text2.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (array3.Length != 5 || !text2.Contains("LISTENING")) continue;
            var num = int.Parse(array3[4]);
            if (num > 0)
            {
                list.Add(num);
            }
        }

        return list.Distinct().ToList();
    }
}