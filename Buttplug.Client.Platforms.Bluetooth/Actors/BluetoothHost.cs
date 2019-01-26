#region Header
// Buttplug.Client.Platforms.Bluetooth/BluetoothHost.cs - Created on 2019-01-20 at 12:15 AM by Sshado.
// This file is part of Buttplug.io which is BSD-3 licensed.
#endregion

#region Using
using System ;
using System.Composition ;
using System.Threading ;
using System.Threading.Tasks ;

using Buttplug.Client.Platforms.Bluetooth.Native ;
using Buttplug.Client.Platforms.Bluetooth.Platforms ;

using Microsoft.Extensions.Logging ;
using Microsoft.Extensions.Options ;

using PostSharp.Patterns.Model ;
using PostSharp.Patterns.Threading ;

using Serilog ;

using ILogger = Serilog.ILogger ;
#endregion

namespace Buttplug.Client.Platforms.Bluetooth.Actors
{
    /// <summary>
    ///     Uses <see cref="Microsoft.Extensions.Logging.ILoggerFactory" /> for a logging source.
    /// </summary>
    [ Actor ]
    public class BluetoothHost : IBluetoothHost
    {
        #region Properties & Fields
        private readonly ILoggerFactory _factory ;

        [ Reference ] private readonly ILogger _log = Log.ForContext <BluetoothHost> () ;

        [ Reference ] private CommonPlatform _platform ;

        [ Import ]
        [ Reference ]
        private INativeBluetooth _bluetooth { get ; set ; }

        [ Reference ]
        private Timer _timer { get ; set ; }
        #endregion

        #region Constructors
        public BluetoothHost ( IOptions <AppConfig> options, ILoggerFactory factory )
        {
            _platform = options.Value.Platform ;
            _factory  = factory ;
        }
        #endregion

        #region Members
        /// <summary>
        ///     Writes the current time out to Verbose logging.
        /// </summary>
        [ Reentrant ]
        private async Task WriteTime ()
        {
            _log.Verbose ( $"[Bluetooth Host] : {DateTime.UtcNow} on timer loop." ) ;
        }
        #endregion

        #region Types & Implementations
        [ EntryPoint ]
        [ Reentrant ]
        public async Task StartAsync ( CancellationToken cancellationToken )
        {
            _bluetooth =
                ( INativeBluetooth ) _platform.GetPlatformClass ( "Buttplug.Client.Platforms.Bluetooth.Native",
                                                                  "NativeBluetooth" ) ;
            _bluetooth.Initialize () ;


            _timer = new Timer (
                                e => WriteTime (),
                                null,
                                TimeSpan.Zero,
                                TimeSpan.FromMinutes ( 1 ) ) ;
        }

        [ EntryPoint ]
        [ Reentrant ]
        public async Task StopAsync ( CancellationToken cancellationToken )
        {
            _log.Information ( "[Bluetooth Host] : Stopping the Bluetooth Host." ) ;
            _timer.Dispose () ;
        }
        #endregion
    }
}
