using AioCore.Phone.Services;

namespace AioCore.Phone;

public partial class MainPage : ContentPage
{
    public MainPage(IBackgroundService backgroundService, 
        IPushDeviceService deviceService)
    {
        InitializeComponent();
        backgroundService.Start();
        deviceService.ShowMessageAndCatchAction();
    }
}