using System;
using System.Collections.Generic;
using System.Text;
using System.Threading ;
using System.Threading.Tasks ;

using Microsoft.Extensions.Hosting ;
using Microsoft.Extensions.Logging ;

namespace Buttplug.Client.Platforms.Bluetooth.Platforms
{
    /// <summary>
    ///     Uses <see cref="Microsoft.Extensions.Logging.ILoggerFactory"/> for a logging source.
    /// </summary>
    public class BluetoothHostService : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
             
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        readonly ILoggerFactory _factory;

        public BluetoothHostService(ILoggerFactory factory) => _factory = factory ;
    }
}
