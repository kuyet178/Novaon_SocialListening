using AioCore.Domain.Aggregates.PlatformAccountAggregate.MappingModels;
using OpenQA.Selenium.Remote;

namespace AioCore.Services.AutomationServices.FacebookScenarios;

public class FacebookSetPermissionsScenario : IFacebookScenario
{
    private readonly IADBService _adbService;

    public FacebookSetPermissionsScenario(IADBService adbService)
    {
        _adbService = adbService;
    }

    public StepType StepType => StepType.SetPermissions;

    public async Task<bool> ExecuteAsync(RemoteWebDriver driver, FacebookAccount actionModel)
    {
        await _adbService.ExecuteAsync(actionModel.DeviceConnection,
            "shell su -c pm grant com.facebook.katana android.permission.READ_EXTERNAL_STORAGE");
        await _adbService.ExecuteAsync(actionModel.DeviceConnection,
            "shell su -c pm grant com.facebook.katana android.permission.READ_EXTERNAL_STORAGE");
        await _adbService.ExecuteAsync(actionModel.DeviceConnection,
            "shell su -c pm grant com.facebook.katana android.permission.WRITE_EXTERNAL_STORAGE");
        await _adbService.ExecuteAsync(actionModel.DeviceConnection, "shell su -c pm grant com.facebook.katana android.permission.CAMERA");
        await _adbService.ExecuteAsync(actionModel.DeviceConnection,
            "shell su -c pm grant com.facebook.katana android.permission.ACCESS_COARSE_LOCATION");
        await _adbService.ExecuteAsync(actionModel.DeviceConnection,
            "shell su -c pm grant com.facebook.katana android.permission.ACCESS_FINE_LOCATION");
        return true;
    }
}