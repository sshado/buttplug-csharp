#region Header
// Buttplug.Client.Platforms.Bluetooth/IMicroService.cs - Created on 2019-01-17 at 8:49 PM by Sshado.
// This file is part of Buttplug.io which is BSD-3 licensed.
#endregion

#region Using
using System.Threading.Tasks ;

using Buttplug.Client.Platforms.Bluetooth.Composition ;

using JetBrains.Annotations ;
#endregion

namespace Buttplug.Client.Platforms.Bluetooth
{
    /// <summary>
    ///     The interface presented by a microservice which the platform service uses to import in Managed Extensibility
    ///     Framework.
    /// </summary>
    public interface IMicroService
    {
        #region Properties & Fields
        [ NotNull ]
        ActorIdentity Identity { get ; }
        #endregion

        #region Members
        Task <bool> Initialize ( [ NotNull ] IPlatformService coreService ) ;

        /* async */
        [ NotNull ]
        Task HandleMessage ( [ NotNull ] dynamic message ) ;
        #endregion
    }
}
