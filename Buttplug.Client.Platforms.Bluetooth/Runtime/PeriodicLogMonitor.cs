using System;
using System.Threading ;

using Buttplug.Client.Platforms.Bluetooth.Aspects ;

using PostSharp.Patterns.Model ;
using PostSharp.Patterns.Threading ;

using Serilog ;

namespace Buttplug.Client.Platforms.Bluetooth.Runtime
{
    [PrivateThreadAware]
    internal class PeriodicLogMonitor
    {
        [Reference]
        private Timer _timer ;

        private Action _monitorLogAction ;

        /// <summary>
        ///     Pass an action to be performed periodically on the logs to monitor dropped messages.
        ///     Will output to the Serilog <see cref="Serilog.Debugging.SelfLog"/> internal logger.
        /// </summary>
        /// <param name="monitorAction"></param>
        [NullDeadEnd]
        [EntryPoint]
        public void StartMonitoringDroppedMessages(Action monitorAction)
        {
            if (this._timer != null)
            {
                Log.ForContext<PeriodicLogMonitor>().Error("Runtime has somehow called for periodic monitoring more than once.");
                //BluetoothRuntime.Platform.Crash();
                return;
            }

            // Set the check to be performed on the logs.
            _monitorLogAction = monitorAction ;

            // Create and start the timer to loop this check.
            this._timer = new Timer(this.MonitorChecksLogCallback,
                                   null,
                                   10000,
                                   10000);
        }

        [NullDeadEnd]
        [EntryPoint]
        public void StopMonitoringDroppedMessages ()
        {
            if ( this._timer == null )
            {
                Log.ForContext<PeriodicLogMonitor>().Error("Runtime has somehow called to stop periodic monitoring without it being enabled.");
                return ;
            }

            using ( var dispose = new ManualResetEvent ( false ) )
            {
                this._timer.Dispose ( dispose ) ;
                dispose.WaitOne (1000) ;
            }
        }

        /// <summary>
        ///     Perform a periodic log check and handle the result.
        /// </summary>
        [EntryPoint]
        private void MonitorChecksLogCallback (object state) => _monitorLogAction () ;
    }
}
