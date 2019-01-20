using System ;
using System.Threading ;
using System.Threading.Tasks ;

using Microsoft.Extensions.Hosting ;
using Microsoft.Extensions.Logging ;

namespace Buttplug.Client.Platforms.Bluetooth.Actors
{
    /// <summary>
    ///     Uses <see cref="Microsoft.Extensions.Logging.ILoggerFactory"/> for a logging source.
    /// </summary>
    public class BluetoothHost : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        readonly ILoggerFactory _factory;

        public BluetoothHost(ILoggerFactory factory) => _factory = factory ;
    }
}
