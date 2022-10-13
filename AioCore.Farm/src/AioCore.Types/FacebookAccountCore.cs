using System.Net;

namespace AioCore.Types
{
    public class FacebookAccountCore
    {
        public string DeviceConnection { get; set; } = default!;
        
        public string DeviceVersion { get; set; } = default!;
        
        public string DevicePlatform { get; set; } = default!;

        public int AppiumPort { get; set; } = default!;

        public int SystemPort { get; set; } = default!;

        public string Uid { get; set; } = default!;

        public string Password { get; set; } = default!;

        public string TwoFactorCode { get; set; } = default!;

        public string Token { get; set; } = default!;

        public string Cookie { get; set; } = default!;

        public CookieContainer CookieContainer { get; set; } = default!;
    }
}