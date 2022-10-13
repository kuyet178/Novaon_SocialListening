namespace AioCore.Types
{
    public class AppiumCore
    {
        public string DeviceId { get; set; } = default!;

        public string PlatformName { get; set; } = default!;

        public string PlatformVersion { get; set; } = default!;

        public int Port { get; set; }

        public int SystemPort { get; set; }

        public bool Started { get; set; }
    }
}