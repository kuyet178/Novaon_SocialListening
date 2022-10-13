using AioCore.Phone.Services;

namespace AioCore.Phone;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        var services = builder.Services;
        
        services.AddTransient<IBackgroundService, BackgroundService>();
        services.AddTransient<IPushDeviceService, PushDeviceService>();
        services.AddTransient<MainPage>();
        
        builder.UseMauiApp<App>();
        services.AddMauiBlazorWebView();
#if DEBUG
        services.AddBlazorWebViewDeveloperTools();
#endif
        
        

        return builder.Build();
    }
}