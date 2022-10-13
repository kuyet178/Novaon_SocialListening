using AioCore.Domain.Aggregates.PlatformAccountAggregate.MappingModels;
using OpenQA.Selenium.Remote;

namespace AioCore.Services.AutomationServices.FacebookScenarios;

public class FacebookSurfingNewsFeedScenario : IFacebookScenario
{
    public StepType StepType => StepType.SurfingNewsFeed;

    public Task<bool> ExecuteAsync(RemoteWebDriver driver, FacebookAccount actionModel)
    {
        throw new NotImplementedException();
    }
}