#region Header
// Buttplug.Client.Platforms.Bluetooth/AsyncLogEventMonitor.cs - Created on 2019-01-23 at 8:29 PM by Sshado.
// This file is part of Buttplug.io which is BSD-3 licensed.
#endregion

#region Using
using Buttplug.Client.Platforms.Bluetooth.Runtime ;

using PostSharp.Patterns.Threading ;

using Serilog.Debugging ;
using Serilog.Sinks.Async ;
#endregion

namespace Buttplug.Client.Platforms.Bluetooth.Composition
{
    [ PrivateThreadAware ]
    internal class AsyncLogEventMonitor : IAsyncLogEventSinkMonitor
    {
        #region Members
        // Example check: log message to an out of band channel if logging is showing signs of dropped messages
        [ EntryPoint ]
        private void ExecuteAsyncBufferCheck ( IAsyncLogEventSinkInspector inspector )
        {
            var messageCount = inspector.DroppedMessagesCount ;
            if ( messageCount > 0 )
                SelfLog.WriteLine ( "Log buffer dropped {0:messageCount} messages and is in a malformed state.",
                                    messageCount ) ;
            else SelfLog.WriteLine ( "Clean logs." ) ;
        }
        #endregion

        #region Types & Implementations
        public void StartMonitoring ( IAsyncLogEventSinkInspector inspector )
        {
            BluetoothRuntime.AddPeriodicCheck ( () => ExecuteAsyncBufferCheck ( inspector ) ) ;
        }

        public void StopMonitoring ( IAsyncLogEventSinkInspector inspector )
        {
            BluetoothRuntime.StopPeriodicCheck () ;
        }
        #endregion
    }
}
