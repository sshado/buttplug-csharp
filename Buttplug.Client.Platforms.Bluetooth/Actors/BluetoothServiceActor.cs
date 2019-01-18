using System ;
using System.Threading.Tasks ;

using JetBrains.Annotations ;

using PostSharp.Patterns.Threading ;

namespace Buttplug.Client.Platforms.Bluetooth.Actors
{

    [Actor]
    [PublicAPI]
    public class BluetoothServiceActor : IMicroService
    {
        public Task <bool> Initialize ( IPlatformService coreService )
        {
            throw new NotImplementedException () ;
        }

        public string Name { get ; }
        public Task HandleMessage ( dynamic message )
        {
            throw new NotImplementedException () ;
        }
    }
}
