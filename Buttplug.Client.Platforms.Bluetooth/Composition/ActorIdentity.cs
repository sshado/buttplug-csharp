#region Header
// Buttplug.Client.Platforms.Bluetooth/ActorIdentity.cs - Created on 2019-01-20 at 8:16 PM by Sshado.
// This file is part of Buttplug.io which is BSD-3 licensed.
#endregion

#region Using
using PostSharp.Patterns.Threading ;
#endregion

namespace Buttplug.Client.Platforms.Bluetooth.Composition
{
    [ ExplicitlySynchronized ]
    public class ActorIdentity
    {
        #region Properties & Fields
        public readonly string Name ;
        #endregion

        #region Constructors
        public ActorIdentity ( string name )
        {
            Name = name ;
        }
        #endregion
    }
}
