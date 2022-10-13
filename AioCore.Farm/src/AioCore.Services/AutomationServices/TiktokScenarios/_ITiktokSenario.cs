using AioCore.Domain.Aggregates.PlatformAccountAggregate.MappingModels;
using AntDesign;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AioCore.Services.AutomationServices.TiktokScenarios;

public enum StepType
{
    Undefined = 0,
    ClearData,
    OpenAPK,
    OpenProfile,
    SurfingNewsFeed
}

public interface ITiktokScenario
{
    StepType StepType { get; }

    Task<bool> ExecuteAsync(RemoteWebDriver driver, TiktokAccount actionModel);
}

