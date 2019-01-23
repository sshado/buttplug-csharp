#region Header
// Buttplug.Client.Platforms.Bluetooth/Win32Bluetooth.cs - Created on 2019-01-20 at 9:36 PM by Sshado.
// This file is part of Buttplug.io which is BSD licensed.
#endregion

using System ;
using System.Linq ;
using System.Runtime.InteropServices ;
using System.Text.RegularExpressions ;
using System.Threading.Tasks ;

using Buttplug.Client.Platforms.Bluetooth.Actors ;
using Buttplug.Client.Platforms.Bluetooth.Native.Win32NT.UWP ;
using Buttplug.Client.Platforms.Bluetooth.Runtime ;

using PostSharp.Patterns.Diagnostics ;
using PostSharp.Patterns.Model ;

using Serilog ;
using Serilog.Core ;

using Microsoft.Win32;

namespace Buttplug.Client.Platforms.Bluetooth.Native.Win32NT
{
    public class NativeBluetooth : INativeBluetooth
    {
        [Reference]
        private readonly ILogger _log = Log.Logger ;

        [Child]
        private EmbeddedClient _adapter { get ; set ; }

        private readonly string name = "Native Bluetooth" ;

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
                var unsafeVersions = new UnsaefUwpVersions().Versions() ;
                if (unsafeVersions.Any( version => version == longName ))
                    Terminate($"Recognized that the environment is running in an unsupported version of Windows 10. Please upgrade." +
                              $" Unsafe [ {string.Join(" ; ", unsafeVersions)} ]");

                _log.Verbose("{name} : Recognized OS as {longName}", name, longName);

                await ChooseWindowsAdapter();
            }
        }

        private async Task ChooseWindowsAdapter ()
        {
            var version = RuntimeInformation.OSDescription;
            string win10Pattern = @"Windows 10\." ;
            var match = Regex.Match ( version, win10Pattern ) ;
            if (match.Success)
                _adapter = new EmbeddedClient();
            else
                _adapter = new EmbeddedClient();

            await _adapter?.Entry () ;
            await _adapter?.StartScanning () ;

        }

        private void UniversalWindowsPlatform ()
        {

        }

        public void Terminate (string reason)
        {
            _log.Fatal("{name} : Terminating from within the native bluetooth class. {reason}", name, reason);
        }

        public IAdapter Adapter { get ; }
        public bool Restart ()
        {
            throw new System.NotImplementedException () ;
        }
    }
}
