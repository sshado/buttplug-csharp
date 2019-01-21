using System;
using System.Collections.Generic;
using System.Text;

using PostSharp.Patterns.Threading ;

namespace Buttplug.Client.Platforms.Bluetooth.Composition
{
    [ExplicitlySynchronized]
    public class ActorIdentity
    {
        public readonly string Name ;

        public ActorIdentity ( string name )
        {
            Name = name ;
        }
    }
}
