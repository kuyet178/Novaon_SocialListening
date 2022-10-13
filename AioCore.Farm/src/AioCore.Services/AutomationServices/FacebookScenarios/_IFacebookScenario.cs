using AioCore.Domain.Aggregates.PlatformAccountAggregate.MappingModels;
using OpenQA.Selenium.Remote;

namespace AioCore.Services.AutomationServices.FacebookScenarios;

public enum StepType
{
    Undefined = 0,
    SetPermissions,
    ClearData,
    OpenAPK,
    Login,
    Enter2FA,
    Restore,
    AccountBackup,
    AcceptFriendRequest,
    SurfingNewsFeed
}

public interface IFacebookScenario
{
    StepType StepType { get; }

    Task<bool> ExecuteAsync(RemoteWebDriver driver, FacebookAccount actionModel);

}