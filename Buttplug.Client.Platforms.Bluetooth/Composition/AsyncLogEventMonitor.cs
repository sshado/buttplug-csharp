using System;
using System.Collections.Generic;
using System.Text;

using Buttplug.Client.Platforms.Bluetooth.Runtime ;

using PostSharp.Patterns.Threading ;

using Serilog.Debugging ;
using Serilog.Sinks.Async ;

namespace Buttplug.Client.Platforms.Bluetooth.Composition
{
    [PrivateThreadAware]
    class AsyncLogEventMonitor : IAsyncLogEventSinkMonitor
    {
        // Example check: log message to an out of band channel if logging is showing signs of dropped messages
        [EntryPoint]
        void ExecuteAsyncBufferCheck(IAsyncLogEventSinkInspector inspector)
        {
            var messageCount = inspector.DroppedMessagesCount;
            if (messageCount > 0) SelfLog.WriteLine("Log buffer dropped {0:messageCount} messages and is in a malformed state.", messageCount);
            else SelfLog.WriteLine("Clean logs.");
        }

        public void StartMonitoring(IAsyncLogEventSinkInspector inspector) =>
            BluetoothRuntime.AddPeriodicCheck(() => ExecuteAsyncBufferCheck(inspector));

        public void StopMonitoring ( IAsyncLogEventSinkInspector inspector ) => 
            BluetoothRuntime.StopPeriodicCheck () ;
    }

}
