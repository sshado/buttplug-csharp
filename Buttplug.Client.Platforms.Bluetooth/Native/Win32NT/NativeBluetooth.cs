#region Header
// Buttplug.Client.Platforms.Bluetooth/Win32Bluetooth.cs - Created on 2019-01-20 at 9:36 PM by Sshado.
// This file is part of Buttplug.io which is BSD licensed.
#endregion

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
            _log.Warning("Reached a Not-Implemented-Exception boundary.");
            return false ;
        }

        public void Terminate ()
        {
            throw new System.NotImplementedException () ;
        }

        public IAdapter Adapter { get ; }
        public bool Restart ()
        {
            throw new System.NotImplementedException () ;
        }
    }
}
