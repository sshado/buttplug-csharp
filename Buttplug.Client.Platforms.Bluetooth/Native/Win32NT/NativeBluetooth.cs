#region Header
// Buttplug.Client.Platforms.Bluetooth/Win32Bluetooth.cs - Created on 2019-01-20 at 9:36 PM by Sshado.
// This file is part of Buttplug.io which is BSD licensed.
#endregion

using Buttplug.Client.Platforms.Bluetooth.Runtime ;

namespace Buttplug.Client.Platforms.Bluetooth.Native.Win32NT
{
    public class NativeBluetooth : INativeBluetooth
    {
        public bool Initialize ()
        {
            throw new System.NotImplementedException () ;
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
