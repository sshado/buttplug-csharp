using System ;
using System.Composition ;
using System.Threading ;
using System.Threading.Tasks ;

using Buttplug.Client.Platforms.Bluetooth.Native ;
using Buttplug.Client.Platforms.Bluetooth.Platforms ;
using Buttplug.Client.Platforms.Bluetooth.Runtime ;

using Microsoft.Extensions.Hosting ;
using Microsoft.Extensions.Logging ;

using PostSharp.Patterns.Model ;
using PostSharp.Serialization ;


using Serilog ;

using ILogger = Serilog.ILogger ;

namespace Buttplug.Client.Platforms.Bluetooth.Actors
{
    /// <summary>
    ///     Uses <see cref="Microsoft.Extensions.Logging.ILoggerFactory"/> for a logging source.
    /// </summary>
    public class BluetoothHost : IHostedService
    {
        [Import]
        [Reference]
        private INativeBluetooth _bluetooth { get ; set ; }

        public Task StartAsync(CancellationToken cancellationToken)
        {
             this._bluetooth =
                 (INativeBluetooth) _platform.GetPlatformClass ( "Buttplug.Client.Platforms.Bluetooth.Native", "NativeBluetooth" ) ;
            return Task.CompletedTask ;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        readonly ILoggerFactory _factory ;

        public BluetoothHost(ILoggerFactory factory) => _factory = factory ;

        [ Reference ] private CommonPlatform _platform ;

        internal async Task Initialize (CommonPlatform platform )
        {
            _platform = platform ;
        }

        [Reference]
        private readonly ILogger _log = Log.ForContext <BluetoothHost>() ;
    }
}
