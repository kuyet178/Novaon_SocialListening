using AioCore.Domain.Aggregates.PlatformAccountAggregate.MappingModels;
using AioCore.Shared.Common.Constants;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AioCore.Services.AutomationServices.TiktokScenarios;
public class TiktokOpenProfileScenario : ITiktokScenario
{
    private readonly IADBService _adbService;

    public TiktokOpenProfileScenario(IADBService adbService)
    {
        _adbService = adbService;
    }

    public StepType StepType => StepType.OpenProfile;

    public async Task<bool> ExecuteAsync(RemoteWebDriver driver, TiktokAccount actionModel)
    {
        
        string urlProfile = "https://www.tiktok.com/@vatvostudio";
        string command = "shell am start -a android.intent.action.VIEW -d " + urlProfile;
        await _adbService.ExecuteAsync(actionModel.DeviceConnection, command);
        return true;
    }
}
