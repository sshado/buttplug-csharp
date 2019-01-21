using System ;
using System.Threading.Tasks ;

using Buttplug.Client.Platforms.Bluetooth.Composition ;

using JetBrains.Annotations ;

using PostSharp.Patterns.Model ;
using PostSharp.Patterns.Threading ;

namespace Buttplug.Client.Platforms.Bluetooth.Actors
{

    [Actor]
    [PublicAPI]
    public class BluetoothServiceActor : IMicroService
    {
        [ Reentrant ]
        public async Task <bool> Initialize ( IPlatformService coreService )
        {
            throw new NotImplementedException () ;
        }

        [ Reference ]
        public ActorIdentity Identity { get ; }

        [ Reentrant ]
        public async Task HandleMessage ( dynamic message )
        {
            throw new NotImplementedException () ;
        }
    }
}
