using System.Text.RegularExpressions;
using AioCore.Domain.Aggregates.PlatformAccountAggregate.MappingModels;
using AioCore.Domain.Common;
using AioCore.Domain.DbContexts;
using AioCore.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium.Remote;

namespace AioCore.Services.AutomationServices.FacebookScenarios;

public class FacebookAccountBackupScenario : IFacebookScenario
{
    private readonly AppSettings _appSettings;
    private readonly IADBService _adbService;
    private readonly SettingsContext _settingsContext;

    public FacebookAccountBackupScenario(AppSettings appSettings,
        IADBService adbService, SettingsContext settingsContext)
    {
        _appSettings = appSettings;
        _adbService = adbService;
        _settingsContext = settingsContext;
    }

    public StepType StepType => StepType.AccountBackup;

    public async Task<bool> ExecuteAsync(RemoteWebDriver driver, FacebookAccount actionModel)
    {
        var uid = string.Empty;
        await _adbService.ExecuteAsync(actionModel.DeviceConnection,
            "cat /data/data/com.facebook.katana/app_light_prefs/com.facebook.katana/authentication");
        var text = await _adbService.ExecuteAsync(actionModel.DeviceConnection,
            $"pull /data/data/com.facebook.katana/app_light_prefs/com.facebook.katana/authentication \"{_appSettings.AssemblyPath}/pulled-{actionModel.DeviceConnection}.txt\"");

        var facebookAccount = await _settingsContext.PlatformAccounts.FirstOrDefaultAsync(
            x => x.UniqueId.Equals(actionModel.Uid));

        if (facebookAccount is null) return false;

        if (!text.Contains("pulled,") ||
            !File.Exists(_appSettings.AssemblyPath + $"/pulled-{actionModel.DeviceConnection}.txt")) return false;
        var input = await File.ReadAllTextAsync(_appSettings.AssemblyPath + $"/pulled-{actionModel.DeviceConnection}.txt");
        var regex = new Regex("c_user\",\"value\":\"([0-9]+)\"");
        if (regex.Match(input).Success)
        {
            uid = regex.Match(input).Groups[1].Value;
            File.Delete(_appSettings.AssemblyPath + $"/pulled-{actionModel.DeviceConnection}.txt");
        }

        if (!Directory.Exists("Authentication"))
        {
            Directory.CreateDirectory("Authentication");
        }

        var text2 = _appSettings.AssemblyPath + "\\" + $"Authentication\\{uid}\\";
        if (!Directory.Exists(text2))
        {
            Directory.CreateDirectory(text2);
        }

        await _adbService.ExecuteAsync(actionModel.DeviceConnection,
            " shell \"su -c ' rm /data/data/com.facebook.katana/app_light_prefs/com.facebook.katana/App*'\"");
        await _adbService.ExecuteAsync(actionModel.DeviceConnection,
            string.Format(
                "pull /data/data/com.facebook.katana/app_light_prefs/com.facebook.katana/ \"{1}/Authentication/{0}\"",
                uid, _appSettings.AssemblyPath));
        await _adbService.ExecuteAsync(actionModel.DeviceConnection,
            string.Format("pull /data/data/com.facebook.katana/databases/ \"{1}/Authentication/{0}/\"", uid,
                _appSettings.AssemblyPath));
        var text3 = await _adbService.ExecuteAsync(actionModel.DeviceConnection,
            string.Format(
                "pull /data/data/com.facebook.katana/app_sessionless_gatekeepers/ \"{1}/Authentication/{0}\"",
                uid, _appSettings.AssemblyPath));
        if (text3.Contains("files pulled"))
        {
            await _adbService.ToastAsync(actionModel.DeviceConnection, "Account backup successful");
        }

        if (File.Exists(text2 + "\\com.facebook.katana\\authentication"))
        {
            var katanaCookieFromBackup =
                GetCookieFromBackup(text2 + "\\com.facebook.katana\\authentication");
            if (katanaCookieFromBackup.Cookie != "" && katanaCookieFromBackup.Token != "")
            {
                facebookAccount.UpdateFacebook(katanaCookieFromBackup.Cookie, katanaCookieFromBackup.Token);
            }
        }

        await _settingsContext.SaveChangesAsync();

        return false;
    }

    private static FacebookAccount GetCookieFromBackup(string sourceFile)
    {
        var fBItems = new FacebookAccount();
        var input = File.ReadAllText(sourceFile);
        var regex = new Regex("access_token(.*?)analytics_claim");
        var match = regex.Match(input);
        if (!match.Success) return fBItems;
        var array = match.Groups[1].Value.Split("\u0005\0\u000f\u0003".ToCharArray(),
            StringSplitOptions.RemoveEmptyEntries);
        var array2 = array[0].ToCharArray();
        var text = array2.Where(value => StringExtensions.ValidChar.Contains(value))
            .Aggregate("", (current, value) => current + value);

        fBItems.Token = text;
        if (array.Length > 2)
        {
            fBItems.Uid = array[2];
        }

        if (array.Length <= 4) return fBItems;
        var authenticationData = "";
        foreach (var t in array)
        {
            if (!t.Contains("c_user")) continue;
            authenticationData = t[1..];
            break;
        }

        if (authenticationData.Contains('['))
        {
            authenticationData = authenticationData[authenticationData.IndexOf("[", StringComparison.Ordinal)..];
        }

        var val = authenticationData.ParseTo<dynamic>();
        var values = new List<string>();
        for (var j = 0; j < val.Length; j++)
        {
            var val2 = val[j]["name"];
            var val3 = val[j]["value"];
            values.Add(val2 + "=" + val3);
            var cookie = new System.Net.Cookie
            {
                Name = val2,
                Value = val3
            };
            if (val[j].ContainsKey("domain"))
            {
                cookie.Domain = val[j]["domain"];
            }

            if (val[j].ContainsKey("path"))
            {
                cookie.Path = val[j]["path"];
            }

            if (val[j].ContainsKey("secure"))
            {
                cookie.Secure = val[j]["secure"];
            }

            if (val[j].ContainsKey("httponly"))
            {
                cookie.HttpOnly = val[j]["httponly"];
            }

            fBItems.CookieContainer.Add(cookie);
        }

        fBItems.Cookie = string.Join("; ", values);

        return fBItems;
    }
}