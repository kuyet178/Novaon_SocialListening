using AioCore.Types;
using System.Net;
using AioCore.Phone.Platforms.Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Net.Wifi;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Widget;
using Newtonsoft.Json;
using RestSharp;

#pragma warning disable CS0618

namespace AioCore.Phone.Services;

[Service(ForegroundServiceType = ForegroundService.TypeDataSync)]
public class BackgroundService : Service, IBackgroundService
{
    public override IBinder OnBind(Intent intent)
    {
        throw new NotImplementedException();
    }

    [return: GeneratedEnum]
    public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
    {
        switch (intent.Action)
        {
            case "START_SERVICE":
                System.Diagnostics.Debug.WriteLine("Start");
                RegisterNotification();
                ListeningDevice();
                break;
            case "STOP_SERVICE":
                System.Diagnostics.Debug.WriteLine("Stop");
                StopForeground(true);
                StopSelfResult(startId);
                break;
        }

        return StartCommandResult.Sticky;
    }

    public void Start()
    {
        Toast.MakeText(MainActivity.ActivityCurrent, "Starting...", ToastLength.Long)?.Show();
        var startService = new Intent(MainActivity.ActivityCurrent, typeof(BackgroundService));
        startService.SetAction("START_SERVICE");
        MainActivity.ActivityCurrent.StartService(startService);
    }

    public void Stop()
    {
        var stopIntent = new Intent(MainActivity.ActivityCurrent, Class);
        stopIntent.SetAction("STOP_SERVICE");
        MainActivity.ActivityCurrent.StartService(stopIntent);
    }

    private void RegisterNotification()
    {
        var channel =
            new NotificationChannel(nameof(BackgroundService), "Starting...", NotificationImportance.Max);
        var manager =
            (NotificationManager)MainActivity.ActivityCurrent.GetSystemService(NotificationService);
        manager?.CreateNotificationChannel(channel);
        var notification = new Notification.Builder(this, nameof(BackgroundService))
            .SetContentTitle("Starting...")
            .SetOngoing(true)
            .Build();

        StartForeground(100, notification);
    }

    private static void ListeningDevice()
    {
        var deviceInfo = DeviceCore();
        var restClient = new RestClient("http://localhost:5108");
        var restRequest = new RestRequest("api/ping", Method.Post);
        restRequest.AddParameter("application/json",
            JsonConvert.SerializeObject(deviceInfo),
            ParameterType.RequestBody);
        restClient.Execute(restRequest);
    }

    private static DeviceCore DeviceCore()
    {
        var context = Android.App.Application.Context;
        var telephonyService = context.GetSystemService(TelephonyService);
        var wifiManager = context.GetSystemService(WifiService);
        if (telephonyService is null || wifiManager is null) return default!;
        var deviceId = Settings.Secure.GetString(context.ContentResolver, Settings.Secure.AndroidId) ?? string.Empty;

        var dhcpInfo = ((WifiManager)wifiManager).DhcpInfo;
        var ipAddress = new IPAddress(BitConverter.GetBytes(dhcpInfo?.IpAddress ?? 0)).ToString();
        var gateway = new IPAddress(BitConverter.GetBytes(dhcpInfo?.Gateway ?? 0)).ToString();
        var netmask = new IPAddress(BitConverter.GetBytes(dhcpInfo?.Netmask ?? 0)).ToString();
        var dns1 = new IPAddress(BitConverter.GetBytes(dhcpInfo?.Dns1 ?? 0)).ToString();
        var dns2 = new IPAddress(BitConverter.GetBytes(dhcpInfo?.Dns2 ?? 0)).ToString();
        var server = new IPAddress(BitConverter.GetBytes(dhcpInfo?.ServerAddress ?? 0)).ToString();
        var lease = dhcpInfo?.LeaseDuration ?? 0;
        var buildNumber = Build.Id ?? string.Empty;
        var board = Build.Board ?? string.Empty;
        var device = Build.Device ?? string.Empty;
        var model = Build.Model ?? string.Empty;
        var brand = Build.Brand ?? string.Empty;
        var bootloader = Build.Bootloader ?? string.Empty;
        var display = Build.Display ?? string.Empty;
        var fingerprint = Build.Fingerprint ?? string.Empty;
        var hardware = Build.Hardware ?? string.Empty;
        var host = Build.Host ?? string.Empty;
        var manufacturer = Build.Manufacturer ?? string.Empty;
        var product = Build.Product ?? string.Empty;
        var type = Build.Type ?? string.Empty;
        var user = Build.User ?? string.Empty;
        var deviceCore = new DeviceCore
        {
            Id = deviceId,
            IPAddress = ipAddress,
            Gateway = gateway,
            Netmask = netmask,
            Dns1 = dns1,
            Dns2 = dns2,
            Server = server,
            LeaseDuration = lease,
            Board = board,
            Device = device,
            Model = model,
            Brand = brand,
            Bootloader = bootloader,
            Display = display,
            FingerPrint = fingerprint,
            Hardware = hardware,
            Host = host,
            Manufacturer = manufacturer,
            Product = product,
            Timestamp = DateTime.Now,
            Type = type,
            User = user,
            BuildNumber = buildNumber
        };
        return deviceCore;
    }
}