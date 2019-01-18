#region Header
// Buttplug.Client.Platforms.Bluetooth/IRuntimeArea.cs - Created on 2019-01-16 at 10:38 PM by Sshado.
// This file is part of Buttplug.io which is BSD licensed.
#endregion

namespace Buttplug.Client.Platforms.Bluetooth.Composition
{
    public interface IRuntimeArea
    {
        /// <summary>
        ///     Composes the runtime area and returns true if there were no issues in runtime.
        /// </summary>
        /// <returns></returns>
        bool Compose () ;
    }
}
