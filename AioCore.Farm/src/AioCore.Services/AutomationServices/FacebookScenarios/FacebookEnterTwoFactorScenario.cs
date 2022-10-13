using AioCore.Domain.Aggregates.PlatformAccountAggregate.MappingModels;
using OpenQA.Selenium.Remote;

namespace AioCore.Services.AutomationServices.FacebookScenarios;

public class FacebookEnterTwoFactorScenario : IFacebookScenario
{
    public StepType StepType => StepType.Enter2FA;

    public Task<bool> ExecuteAsync(RemoteWebDriver driver, FacebookAccount actionModel)
    {
        throw new NotImplementedException();
    }
}