using AioCore.Services.CommonServices;
using AioCore.Types;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Remote;

namespace AioCore.Services.AutomationServices;

public interface IAppiumService
{
    Task<AppiumCore> StartAsync(string deviceId, int port);

    Task<RemoteWebDriver> RemoteAsync(string deviceId, AppiumCore config);
}

public class AppiumService : IAppiumService
{
    private readonly ICommandService _commandService;
    private readonly ILogger<AppiumService> _logger;

    public AppiumService(ICommandService commandService,
        ILogger<AppiumService> logger)
    {
        _commandService = commandService;
        _logger = logger;
    }

    public async Task<AppiumCore> StartAsync(string deviceId, int port)
    {
        var num = 0;
        var appiumConfig = new AppiumCore
        {
            DeviceId = deviceId,
            Port = port
        };
        new Task(delegate { _commandService.ExecuteAsync($"appium --port {port}"); }).Start();
        while (num < 20)
        {
            var allPortListeningOnComputer = await _commandService.NetstatListening();
            Thread.Sleep(1000);
            num++;
            if (!allPortListeningOnComputer.Contains(port.ToString())) continue;
            appiumConfig.Started = true;
            _logger.LogInformation("{Date}: Appium Server Started", DateTime.Now);
            return appiumConfig;
        }

        return appiumConfig;
    }

    public async Task<RemoteWebDriver> RemoteAsync(string deviceId, AppiumCore config)
    {
        var appiumOptions = new AppiumOptions();
        appiumOptions.AddAdditionalAppiumOption(MobileCapabilityType.PlatformName, config.PlatformName);
        appiumOptions.AddAdditionalAppiumOption("unicodeKeyboard", true);
        appiumOptions.AddAdditionalAppiumOption("systemPort", config.SystemPort);
        appiumOptions.AddAdditionalAppiumOption("udid", config.DeviceId);
        appiumOptions.AddAdditionalAppiumOption("devicePort", config.Port);
        appiumOptions.AddAdditionalAppiumOption("noReset", true);
        appiumOptions.AddAdditionalAppiumOption("disableWindowAnimation", true);
        appiumOptions.AddAdditionalAppiumOption("skipServerInstallation", false);
        appiumOptions.AddAdditionalAppiumOption("skipDeviceInitialization", false);
        appiumOptions.AddAdditionalAppiumOption("gpsEnabled", false);
        appiumOptions.AddAdditionalAppiumOption("skipLogcatCapture", true);
        appiumOptions.AddAdditionalAppiumOption("dontStopAppOnReset", true);
        appiumOptions.AddAdditionalAppiumOption("ignoreHiddenApiPolicyError", true);
        appiumOptions.AddAdditionalAppiumOption("noSign", false);
        appiumOptions.AddAdditionalAppiumOption("newCommandTimeout", 360000);

        var openPorts = await _commandService.NetstatListening();
        var num = 0;
        while (num < 5 && !openPorts.Contains(config.Port.ToString()))
        {
            num++;
            if (!openPorts.Contains(config.Port.ToString()))
            {
                var appiumInfo = await StartAsync(deviceId, config.Port);
                if (appiumInfo.Started)
                {
                    openPorts = await _commandService.NetstatListening();
                    break;
                }
            }

            Thread.Sleep(1000);
        }

        if (!openPorts.Contains(config.Port.ToString()))
        {
            // TODO: Chuyển trạng thái không khởi động được appium về server
        }

        var hub = new Uri("http://127.0.0.1:" + config.Port + "/wd/hub");
        var remoteWebDriver = new RemoteWebDriver(hub, appiumOptions);
        remoteWebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5.0);
        _logger.LogInformation("{Date}: RemoteWebDriver Remote To Appium With Port {Port} successful", 
            DateTime.Now, config.Port);

        return remoteWebDriver;
    }
}