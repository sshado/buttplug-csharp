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
using PostSharp.Patterns.Threading ;
using PostSharp.Serialization ;


using Serilog ;

using ILogger = Serilog.ILogger ;

namespace Buttplug.Client.Platforms.Bluetooth.Actors
{
    /// <summary>
    ///     Uses <see cref="Microsoft.Extensions.Logging.ILoggerFactory"/> for a logging source.
    /// </summary>
    [Actor]
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

        [EntryPoint]
        [Reentrant]
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            this._bluetooth =
                (INativeBluetooth) _platform.GetPlatformClass ( "Buttplug.Client.Platforms.Bluetooth.Native", "NativeBluetooth" ) ;
            this._bluetooth.Initialize () ;
            
            
            _timer = new Timer(
                               (e) => WriteTime(),
                               null,
                               TimeSpan.Zero,
                               TimeSpan.FromMinutes(1));
        }

        [EntryPoint]
        [Reentrant]
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _log.Information("[Bluetooth Host] : Stopping the Bluetooth Host.");
            _timer.Dispose () ;
        }

        private readonly ILoggerFactory _factory ;

        [ Reference ]
        private Timer _timer { get ; set ; }

        [ Reference ] private CommonPlatform _platform ;

        [ Reference ]
        private readonly ILogger _log = Log.ForContext <BluetoothHost>() ;

        /// <summary>
        ///     Writes the current time out to Verbose logging.
        /// </summary>
        [ Reentrant ]
        private async Task WriteTime()
        {
            _log.Verbose($"[Bluetooth Host] : {DateTime.UtcNow} on timer loop.");
        }
    }
}
