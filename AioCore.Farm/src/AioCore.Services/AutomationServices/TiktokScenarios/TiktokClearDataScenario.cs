using AioCore.Domain.Aggregates.PlatformAccountAggregate.MappingModels;
using AioCore.Services.AutomationServices.FacebookScenarios;
using AioCore.Shared.Common.Constants;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AioCore.Services.AutomationServices.TiktokScenarios;
public class TiktokClearDataScenario : ITiktokScenario
{
    private readonly IADBService _adbService;

    public TiktokClearDataScenario(IADBService adbService)
    {
        _adbService = adbService;
    }

    public StepType StepType => StepType.ClearData;

    public async Task<bool> ExecuteAsync(RemoteWebDriver driver, TiktokAccount actionModel)
    {
        await _adbService.ClearAppDataAsync(actionModel.DeviceConnection, PackageNames.TiktokName);
        return true;
    }
}
