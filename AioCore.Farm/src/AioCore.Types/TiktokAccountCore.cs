using System;
using System.Collections.Generic;
using System.Text;

namespace AioCore.Types
{
    public class TiktokAccountCore
    {
        public string DeviceConnection { get; set; } = default!;

        public string DeviceVersion { get; set; } = default!;

        public string DevicePlatform { get; set; } = default!;

        public int AppiumPort { get; set; } = default!;

        public int SystemPort { get; set; } = default!;


    }
}
