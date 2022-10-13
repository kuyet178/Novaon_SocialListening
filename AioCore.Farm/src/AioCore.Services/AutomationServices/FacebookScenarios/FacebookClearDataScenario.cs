using AioCore.Domain.Aggregates.PlatformAccountAggregate.MappingModels;
using AioCore.Shared.Common.Constants;
using OpenQA.Selenium.Remote;

namespace AioCore.Services.AutomationServices.FacebookScenarios;

public class FacebookClearDataScenario : IFacebookScenario
{
    private readonly IADBService _adbService;

    public FacebookClearDataScenario(IADBService adbService)
    {
        _adbService = adbService;
    }

    public StepType StepType => StepType.ClearData;

    public async Task<bool> ExecuteAsync(RemoteWebDriver driver, FacebookAccount actionModel)
    {
        await _adbService.ClearAppDataAsync(actionModel.DeviceConnection, PackageNames.Facebook);
        return true;
    }
}