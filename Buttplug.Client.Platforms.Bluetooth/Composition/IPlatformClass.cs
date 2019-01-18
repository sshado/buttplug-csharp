using System;
using System.Collections.Generic;
using System.Text;

namespace Buttplug.Client.Platforms.Bluetooth.Platforms
{
    /// <summary>
    ///     Supports an extremely limited feature set common to every supported runtime.
    /// </summary>
    public interface IPlatformClass
    {
        /// <summary>
        ///     Must perform platform-specific initialization in preparation to serve a microservice.
        /// </summary>
        /// <returns>True on successful platform class initialization; false otherwise.</returns>
        bool Initialize();

        /// <summary>
        ///     Shutdown and perform any last minute procedures specific to the platform.
        /// </summary>
        void Terminate();
    }
}
