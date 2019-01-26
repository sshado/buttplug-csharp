#region Header
// Buttplug.Client.Platforms.Bluetooth/IPlatformClass.cs - Created on 2019-01-17 at 8:49 PM by Sshado.
// This file is part of Buttplug.io which is BSD-3 licensed.
#endregion

namespace Buttplug.Client.Platforms.Bluetooth.Platforms
{
    /// <summary>
    ///     Supports an extremely limited feature set common to every supported runtime.
    /// </summary>
    public interface IPlatformClass
    {
        #region Members
        /// <summary>
        ///     Must perform platform-specific initialization in preparation to serve a microservice.
        /// </summary>
        /// <returns>True on successful platform class initialization; false otherwise.</returns>
        bool Initialize () ;

        /// <summary>
        ///     Shutdown and perform any last minute procedures specific to the platform.
        /// </summary>
        void Terminate ( string reason ) ;
        #endregion
    }
}
