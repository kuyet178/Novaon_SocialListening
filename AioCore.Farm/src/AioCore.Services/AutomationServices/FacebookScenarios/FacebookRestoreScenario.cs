using AioCore.Domain.Aggregates.PlatformAccountAggregate.MappingModels;
using AioCore.Domain.Common;
using OpenQA.Selenium.Remote;

namespace AioCore.Services.AutomationServices.FacebookScenarios;

public class FacebookRestoreScenario : IFacebookScenario
{
    private readonly IADBService _adbService;
    private readonly AppSettings _appSettings;

    public FacebookRestoreScenario(IADBService adbService, AppSettings appSettings)
    {
        _adbService = adbService;
        _appSettings = appSettings;
    }

    public StepType StepType => StepType.Restore;

    public async Task<bool> ExecuteAsync(RemoteWebDriver driver, FacebookAccount actionModel)
    {
        if (!Directory.Exists("Authentication"))
        {
            Directory.CreateDirectory("Authentication");
        }

        var path = $"Authentication\\{actionModel.Uid}";
        if (!Directory.Exists(path)) return false;
        await _adbService.ExecuteAsync(actionModel.DeviceConnection,
            "shell \"su -c ' rm /data/data/com.facebook.katana/app_light_prefs/com.facebook.katana/App*'\"");
        await _adbService.ExecuteAsync(actionModel.DeviceConnection,
            string.Format(
                "push \"{1}/Authentication/{0}/com.facebook.katana\" /data/data/com.facebook.katana/app_light_prefs/",
                actionModel.Uid, _appSettings.AssemblyPath));
        await _adbService.ExecuteAsync(actionModel.DeviceConnection,
            string.Format(
                "push \"{1}/Authentication/{0}/com.facebook.katana\" /data/data/com.facebook.katana/app_light_prefs/",
                actionModel.Uid, _appSettings.AssemblyPath));
        await _adbService.ExecuteAsync(actionModel.DeviceConnection,
            string.Format("push \"{1}/Authentication/{0}/databases/\" /data/data/com.facebook.katana/",
                actionModel.Uid, _appSettings.AssemblyPath));
        var text = await _adbService.ExecuteAsync(actionModel.DeviceConnection,
            string.Format(
                "push \"{1}/Authentication/{0}/app_sessionless_gatekeepers/\" /data/data/com.facebook.katana/app_sessionless_gatekeepers/",
                actionModel.Uid, _appSettings.AssemblyPath));
        Thread.Sleep(1000);
        if (!text.Contains("pushed")) return false;
        await _adbService.ExecuteAsync(actionModel.DeviceConnection,
            "shell am start -n com.facebook.katana/.LoginActivity");
        return true;
    }
}