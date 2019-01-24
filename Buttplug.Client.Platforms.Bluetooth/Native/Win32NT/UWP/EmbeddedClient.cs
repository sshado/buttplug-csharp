using System;
using System.Collections.Generic;
using System.Linq ;
using System.Text;
using System.Threading.Tasks ;

using Buttplug.Client;

using PostSharp.Patterns.Model ;
using PostSharp.Patterns.Threading ;

using Serilog ;

using System.Threading.Tasks;

using PostSharp.Patterns.Diagnostics ;

namespace Buttplug.Client.Platforms.Bluetooth.Native.Win32NT.UWP
{
    [PrivateThreadAware]
    internal class EmbeddedClient
    {
        [Reference]
        private readonly ILogger _log = Log.Logger;

        [Reference]
        [Log(AttributeExclude = true)]
        public ButtplugClient _device { get ; set ; }

        [EntryPoint]
        [Reentrant]
        public async Task Entry ()
        {
            _device = new ButtplugClient("Example Client", new ButtplugEmbeddedConnector("Example Server"));
            await _device.ConnectAsync();
            _device.DeviceAdded += HandleDeviceAdded;
            _device.DeviceRemoved += HandleDeviceRemoved;
        }

        [Reentrant]
        public async Task StartScanning ()
        {
            var scanAndForget = ScanForDevices();
        }

        void HandleDeviceAdded(object aObj, DeviceAddedEventArgs aArgs)
        {
            _log.Information("Device connected: {Name}", aArgs.Device.Name );
        }

        void HandleDeviceRemoved(object aObj, DeviceRemovedEventArgs aArgs)
        {
            _log.Information("Device connected: {Name}", aArgs.Device.Name );
        }

        private async Task ScanForDevices()
        {
            var scan = 5 ;
            _log.Information("Scanning for devices for {scan} seconds. Found devices will be printed to console.", scan);
            _device.StartScanningAsync();
            await Task.Delay(TimeSpan.FromSeconds(scan));
            await _device.StopScanningAsync();


            if (!_device.Devices.Any())
            {
                _log.Information("No devices available. Please scan for a device.");
                return;
            }

            foreach ( var dev in _device.Devices )
            {
                _log.Information ( "{Index}. {Name}", dev.Index, dev.Name ) ;
            }

        }
    }
}
