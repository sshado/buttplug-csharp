using System ;
using System.Threading.Tasks ;

namespace Buttplug.Client.Platforms.Bluetooth.Actors
{

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
