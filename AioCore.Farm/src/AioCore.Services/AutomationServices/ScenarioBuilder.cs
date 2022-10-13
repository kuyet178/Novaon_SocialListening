using AioCore.Domain.Aggregates.PlatformAccountAggregate.MappingModels;
using AioCore.Services.AutomationServices.FacebookScenarios;
using AioCore.Services.CommonServices;
using AioCore.Shared.Common.Constants;
using AioCore.Types;
using Microsoft.AspNetCore.Hosting;
using static AioCore.Services.AutomationServices.ScenarioFactory;

namespace AioCore.Services.AutomationServices;

public class ScenarioBuilder
{
    public class FacebookScenarioBuilder
    {
        private readonly ScenarioFactory.FacebookScenarioFactory _facebookScenarioFactory;
        private readonly IWebHostEnvironment _environment;
        private readonly ICommandService _commandService;
        private readonly IAppiumService _appiumService;
        private readonly IADBService _adbService;

        public FacebookScenarioBuilder(IADBService adbService, IWebHostEnvironment environment,
            ScenarioFactory.FacebookScenarioFactory facebookScenarioFactory,
            ICommandService commandService, IAppiumService appiumService)
        {
            _facebookScenarioFactory = facebookScenarioFactory;
            _commandService = commandService;
            _appiumService = appiumService;
            _environment = environment;
            _adbService = adbService;
        }

        public async Task StartAsync(FacebookAccount facebookAccount, List<FacebookScenarios.StepType> stepTypes)
        {
            if (!await _adbService.InstalledAsync(facebookAccount.DeviceConnection, PackageNames.Facebook))
            {
                var facebookApk = _environment.ContentRootPath + $"\\apks\\{SystemAPKs.Facebook}";
                await _adbService.InstallAppAsync(facebookAccount.DeviceConnection, facebookApk);
                Thread.Sleep(10000);
                await _adbService.SetPortraitAsync(facebookAccount.DeviceConnection);
            }
            else
            {
                await _commandService.KillProcessAsync(facebookAccount.AppiumPort);
                await _commandService.KillProcessAsync(facebookAccount.SystemPort);
                await _appiumService.StartAsync(facebookAccount.DeviceConnection, facebookAccount.AppiumPort);
                var driver = await _appiumService.RemoteAsync(facebookAccount.DeviceConnection, new AppiumCore
                {
                    Port = facebookAccount.AppiumPort,
                    DeviceId = facebookAccount.DeviceConnection,
                    PlatformName = facebookAccount.DevicePlatform,
                    PlatformVersion = facebookAccount.DeviceVersion,
                    SystemPort = facebookAccount.SystemPort
                });

                foreach (var stepType in stepTypes)
                {
                    var processor = _facebookScenarioFactory.GetProcessor(stepType);
                    if (processor is null) continue;
                    await processor.ExecuteAsync(driver, facebookAccount);
                }
            }
        }
    }
    public class TiktokScenarioBuilder
    {
        private readonly ScenarioFactory.TiktokScenarioFactory _tiktokScenarioFactory;
        private readonly IWebHostEnvironment _environment;
        private readonly ICommandService _commandService;
        private readonly IAppiumService _appiumService;
        private readonly IADBService _adbService;

        public TiktokScenarioBuilder(IADBService adbService, IWebHostEnvironment environment,
          ScenarioFactory.TiktokScenarioFactory tiktokScenarioFactory,
          ICommandService commandService, IAppiumService appiumService)
        {
            _tiktokScenarioFactory = tiktokScenarioFactory;
            _commandService = commandService;
            _appiumService = appiumService;
            _environment = environment;
            _adbService = adbService;
        }

        public async Task StartAsync(TiktokAccount tiktokAccount, List<TiktokScenarios.StepType> stepTypes)
        {
            if (!await _adbService.InstalledAsync(tiktokAccount.DeviceConnection, PackageNames.TiktokName))
            {
                var tiktokAPK = _environment.ContentRootPath + $"\\apks\\{SystemAPKs.Tiktok}";
                await _adbService.InstallAppAsync(tiktokAccount.DeviceConnection, tiktokAPK);
                Thread.Sleep(10000);
                await _adbService.SetPortraitAsync(tiktokAccount.DeviceConnection);
            }
            else
            {
                await _commandService.KillProcessAsync(tiktokAccount.AppiumPort);
                await _commandService.KillProcessAsync(tiktokAccount.SystemPort);
                await _appiumService.StartAsync(tiktokAccount.DeviceConnection, tiktokAccount.AppiumPort);
                var driver = await _appiumService.RemoteAsync(tiktokAccount.DeviceConnection, new AppiumCore
                {
                    Port = tiktokAccount.AppiumPort,
                    DeviceId = tiktokAccount.DeviceConnection,
                    PlatformName = tiktokAccount.DevicePlatform,
                    PlatformVersion = tiktokAccount.DeviceVersion,
                    SystemPort = tiktokAccount.SystemPort
                });

                foreach (var stepType in stepTypes)
                {
                    var processor = _tiktokScenarioFactory.GetProcessor(stepType);
                    if (processor is null) continue;
                    Thread.Sleep(5000);
                    await processor.ExecuteAsync(driver, tiktokAccount);
                }
            }
        }
    }
}