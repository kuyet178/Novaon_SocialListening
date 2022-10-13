using AioCore.Phone.Platforms.Android;
using Android.App;

namespace AioCore.Phone.Services;

public class PushDeviceService : IPushDeviceService
{
    public void ShowMessageAndCatchAction()
    {
        var activity = (Activity)MainActivity.ActivityCurrent;
    }
}