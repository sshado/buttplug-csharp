#region Header
// Buttplug.Client.Platforms.Bluetooth/NativeBluetooth.cs - Created on 2019-01-20 at 9:36 PM by Sshado.
// This file is part of Buttplug.io which is BSD licensed.
#endregion

#region Using
using System ;
using System.Linq ;
using System.Runtime.InteropServices ;
using System.Text.RegularExpressions ;
using System.Threading.Tasks ;

using Buttplug.Client.Platforms.Bluetooth.Native.Win32NT.UWP ;

using PostSharp.Patterns.Model ;
using PostSharp.Patterns.Threading ;

using Serilog ;
#endregion

namespace Buttplug.Client.Platforms.Bluetooth.Native.Win32NT
{
    [ PrivateThreadAware ]
    public class NativeBluetooth : INativeBluetooth
    {
        #region Properties & Fields
        [ Reference ] private readonly ILogger _log = Log.ForContext<NativeBluetooth> ( ) ;

        private readonly string name = "Native Bluetooth" ;

        [ Reference ]
        internal EmbeddedClient _adapter { get ; set ; }

        [ Child ]
        public IAdapter Adapter { get ; }
        #endregion

        #region Members
        private async Task ReflectAndConfigure ()
        {
            var systemWindows = RuntimeInformation
               .IsOSPlatform ( OSPlatform.Windows ) ;
            if ( systemWindows )
            {
                var longName       = RuntimeInformation.OSDescription ;
                var unsafeVersions = new UnsafeUwpVersions ().Versions () ;
                if ( unsafeVersions.Any ( version => version == longName ) )
                    Terminate ( "Recognized that the environment is running in an unsupported version of Windows 10. Please upgrade." +
                                $" Unsafe [ {string.Join ( " ; ", unsafeVersions )} ]" ) ;

                _log.Verbose ( "{name} : Recognized OS as {longName}", name, longName ) ;

                await ChooseWindowsAdapter () ;
            }
        }

        /// <summary>
        ///     Begins scanning on the Windows adapter.
        /// </summary>
        private async Task ChooseWindowsAdapter ()
        {
            var version      = RuntimeInformation.OSDescription ;
            var win10Pattern = @"Windows 10\." ;
            var match        = Regex.Match ( version, win10Pattern ) ;
            if ( match.Success )
                _adapter = new EmbeddedClient () ;
            else
                _adapter = new EmbeddedClient () ;

            await _adapter?.Entry () ;
            await _adapter?.StartScanning () ;
        }

        private void UniversalWindowsPlatform () { }
        #endregion

        #region Types & Implementations
        [ EntryPoint ]
        public bool Initialize ()
        {
            var fireThenForget = ReflectAndConfigure () ;
            return true ;
        }

        public void Terminate ( string reason )
        {
            _log.Fatal ( "{name} : Terminating from within the native bluetooth class. {reason}", name, reason ) ;
        }

        public bool Restart ()
        {
            throw new NotImplementedException () ;
        }
        #endregion
    }
}
