using System;
using System.Collections.Generic;
using System.Text;

using Buttplug.Client.Platforms.Bluetooth.Platforms ;

using PostSharp.Patterns.Model ;

namespace Buttplug.Client.Platforms.Bluetooth
{
    public class AppConfig
    {
        [ Reference ]
        public string Options { get ; set ; }
        [ Reference ]
        public CommonPlatform Platform { get ; set ; }
    }
}
