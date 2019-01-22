using System ;
using System.Composition ;
using System.Threading ;
using System.Threading.Tasks ;

using Buttplug.Client.Platforms.Bluetooth.Native ;
using Buttplug.Client.Platforms.Bluetooth.Platforms ;
using Buttplug.Client.Platforms.Bluetooth.Runtime ;

using Microsoft.Extensions.Hosting ;
using Microsoft.Extensions.Logging ;
using Microsoft.Extensions.Options ;

using PostSharp.Patterns.Model ;
using PostSharp.Serialization ;


using Serilog ;

using ILogger = Serilog.ILogger ;

namespace Buttplug.Client.Platforms.Bluetooth.Actors
{
    /// <summary>
    ///     Uses <see cref="Microsoft.Extensions.Logging.ILoggerFactory"/> for a logging source.
    /// </summary>
    public class BluetoothHost : IBluetoothHost
    {
        [Import]
        [Reference]
        private INativeBluetooth _bluetooth { get ; set ; }

        public BluetoothHost (IOptions <AppConfig> options, ILoggerFactory factory)
        {
            _platform = options.Value.Platform ;
            _factory = factory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            this._bluetooth =
                (INativeBluetooth) _platform.GetPlatformClass ( "Buttplug.Client.Platforms.Bluetooth.Native", "NativeBluetooth" ) ;
            this._bluetooth.Initialize () ;
            
            
            _timer = new Timer(
                               (e) => WriteTime(),
                               null,
                               TimeSpan.Zero,
                               TimeSpan.FromMinutes(1));
            return Task.CompletedTask ;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _log.Information("Stopping the Bluetooth Host.");
            _timer.Dispose () ;
            return Task.CompletedTask ;
        }

        private readonly ILoggerFactory _factory ;

        [ Reference ]
        private Timer _timer { get ; set ; }

        [ Reference ] private CommonPlatform _platform ;

        [Reference]
        private readonly ILogger _log = Log.ForContext <BluetoothHost>() ;

        public void WriteTime()
        {
            _log.Verbose($"[Bluetooth Host]: {DateTime.UtcNow} on timer loop.");
        }
    }
}
