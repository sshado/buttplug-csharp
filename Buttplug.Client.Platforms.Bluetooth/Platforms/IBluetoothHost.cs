using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks ;

using Microsoft.Extensions.Hosting ;

namespace Buttplug.Client.Platforms.Bluetooth.Platforms
{
    public interface IBluetoothHost : IHostedService
    {
        Task Initialize ( CommonPlatform platform ) ;
    }
}
