using AioCore.Domain.Aggregates.PlatformAccountAggregate.MappingModels;
using AioCore.Shared.Common.Constants;
using OpenQA.Selenium.Remote;

namespace AioCore.Services.AutomationServices.FacebookScenarios;

public class FacebookOpenAPKScenario : IFacebookScenario
{
    private readonly IADBService _adbService;

    public FacebookOpenAPKScenario(IADBService adbService)
    {
        _adbService = adbService;
    }

    public StepType StepType => StepType.OpenAPK;

    public async Task<bool> ExecuteAsync(RemoteWebDriver driver, FacebookAccount actionModel)
    {
        await _adbService.OpenAppAsync(actionModel.DeviceConnection, PackageNames.Facebook);
        return true;
    }
}