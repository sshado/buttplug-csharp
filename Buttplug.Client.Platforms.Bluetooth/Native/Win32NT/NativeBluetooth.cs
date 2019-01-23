#region Header
// Buttplug.Client.Platforms.Bluetooth/Win32Bluetooth.cs - Created on 2019-01-20 at 9:36 PM by Sshado.
// This file is part of Buttplug.io which is BSD licensed.
#endregion

using System ;
using System.Linq ;
using System.Runtime.InteropServices ;
using System.Threading.Tasks ;

using Buttplug.Client.Platforms.Bluetooth.Actors ;
using Buttplug.Client.Platforms.Bluetooth.Runtime ;

using PostSharp.Patterns.Model ;

using Serilog ;

namespace Buttplug.Client.Platforms.Bluetooth.Native.Win32NT
{
    public class NativeBluetooth : INativeBluetooth
    {
        [Reference]
        private readonly ILogger _log = Log.ForContext <BluetoothHost>() ;

        public bool Initialize ()
        {
            var fireThenForget = ReflectAndConfigure () ;
            return true ;
        }

        private async Task ReflectAndConfigure ()
        {
            bool systemWindows = System.Runtime.InteropServices.RuntimeInformation
                                       .IsOSPlatform(OSPlatform.Windows);
            if (systemWindows)
            {
                var longName = RuntimeInformation.OSDescription;
                if (UnsaefUwpVersions.Versions.Any( version => version == longName ))
                    Terminate($"Recognized that the environment is running in an unsupported version of Windows 10. Please upgrade." +
                              $" Unsafe [ {string.Join(" ; ", UnsaefUwpVersions.Versions)} ]");

                _log.Debug($"Native Bluetooth: Recognized OS as {longName}");
            }
        }

        public void Terminate (string reason)
        {
            _log.Fatal($"Native Bluetooth: Terminating from within the native bluetooth class.{Environment.NewLine}{reason}");
        }

        public IAdapter Adapter { get ; }
        public bool Restart ()
        {
            throw new System.NotImplementedException () ;
        }
    }
}
