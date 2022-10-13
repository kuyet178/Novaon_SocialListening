namespace AioCore.Services.AutomationServices;

public interface IDeviceService
{
    Task<string> AndroidVersion(string deviceId);
}

public class DeviceService : IDeviceService
{
    private readonly IADBService _adbService;

    public DeviceService(IADBService adbService)
    {
        _adbService = adbService;
    }

    public async Task<string> AndroidVersion(string deviceId)
    {
        return await _adbService.ExecuteAsync(deviceId, "shell getprop ro.build.version.release");
    }
}