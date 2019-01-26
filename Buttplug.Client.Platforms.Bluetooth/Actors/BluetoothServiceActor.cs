#region Header
// Buttplug.Client.Platforms.Bluetooth/BluetoothServiceActor.cs - Created on 2019-01-16 at 8:35 PM by Sshado.
// This file is part of Buttplug.io which is BSD-3 licensed.
#endregion

#region Using
using System ;
using System.Threading.Tasks ;

using Buttplug.Client.Platforms.Bluetooth.Composition ;

using JetBrains.Annotations ;

using PostSharp.Patterns.Model ;
using PostSharp.Patterns.Threading ;
#endregion

namespace Buttplug.Client.Platforms.Bluetooth.Actors
{
    [ Actor ]
    [ PublicAPI ]
    public class BluetoothServiceActor : IMicroService
    {
        #region Properties & Fields
        [ Reference ]
        public ActorIdentity Identity { get ; }
        #endregion

        #region Types & Implementations
        [ Reentrant ]
        public async Task <bool> Initialize ( IPlatformService coreService )
        {
            throw new NotImplementedException () ;
        }

        [ Reentrant ]
        public async Task HandleMessage ( dynamic message )
        {
            throw new NotImplementedException () ;
        }
        #endregion
    }
}
