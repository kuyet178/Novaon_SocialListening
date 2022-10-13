using AioCore.Services.AutomationServices.FacebookScenarios;
using AioCore.Services.AutomationServices.TiktokScenarios;
using Microsoft.Extensions.DependencyInjection;

namespace AioCore.Services.AutomationServices;

public static class ScenarioRegister
{
    public static void AddAllScenarios(this IServiceCollection services)
    {
        //Facebook
        services.AddScoped<ScenarioBuilder.FacebookScenarioBuilder>();
        services.AddScoped<ScenarioFactory.FacebookScenarioFactory>();
        services.AddScoped(typeof(IFacebookScenario), typeof(FacebookAcceptFriendRequestScenario));
        services.AddScoped(typeof(IFacebookScenario), typeof(FacebookAccountBackupScenario));
        services.AddScoped(typeof(IFacebookScenario), typeof(FacebookClearDataScenario));
        services.AddScoped(typeof(IFacebookScenario), typeof(FacebookEnterTwoFactorScenario));
        services.AddScoped(typeof(IFacebookScenario), typeof(FacebookLoginScenario));
        services.AddScoped(typeof(IFacebookScenario), typeof(FacebookOpenAPKScenario));
        services.AddScoped(typeof(IFacebookScenario), typeof(FacebookRestoreScenario));
        services.AddScoped(typeof(IFacebookScenario), typeof(FacebookSetPermissionsScenario));
        services.AddScoped(typeof(IFacebookScenario), typeof(FacebookSurfingNewsFeedScenario));

        //Tiktok
        services.AddScoped<ScenarioBuilder.TiktokScenarioBuilder>();
        services.AddScoped<ScenarioFactory.TiktokScenarioFactory>();
        services.AddScoped(typeof(ITiktokScenario), typeof(TiktokClearDataScenario));
        services.AddScoped(typeof(ITiktokScenario), typeof(TiktokOpenAPKScenario));
        services.AddScoped(typeof(ITiktokScenario), typeof(TiktokOpenProfileScenario));
        services.AddScoped(typeof(ITiktokScenario), typeof(TiktokSurfingNewsFeedScenario));

    }
}