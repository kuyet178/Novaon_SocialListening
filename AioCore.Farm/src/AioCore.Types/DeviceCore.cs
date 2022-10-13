using System;

namespace AioCore.Types
{
    public class DeviceCore
    {
        public string Id { get; set; } = default!;

        public string DeviceId { get; set; } = default!;

        public string IPAddress { get; set; } = default!;

        public string Gateway { get; set; } = default!;

        public string Netmask { get; set; } = default!;

        public string Dns1 { get; set; } = default!;

        public string Dns2 { get; set; } = default!;

        public string Server { get; set; } = default!;

        public int LeaseDuration { get; set; }

        public string Board { get; set; } = default!;

        public string Device { get; set; } = default!;

        public string Model { get; set; } = default!;

        public string Brand { get; set; } = default!;

        public string Bootloader { get; set; } = default!;

        public string Display { get; set; } = default!;

        public string FingerPrint { get; set; } = default!;

        public string Hardware { get; set; } = default!;

        public string Host { get; set; } = default!;

        public string Manufacturer { get; set; } = default!;

        public string Product { get; set; } = default!;

        public DateTime Timestamp { get; set; } = DateTime.Now;

        public string Type { get; set; } = default!;

        public string User { get; set; } = default!;

        public string BuildNumber { get; set; } = default!;
    }
}