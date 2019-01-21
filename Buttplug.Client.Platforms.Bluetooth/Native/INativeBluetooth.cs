using Buttplug.Client.Platforms.Bluetooth.Platforms ;

namespace Buttplug.Client.Platforms.Bluetooth.Native
{
    public interface INativeBluetooth : IPlatformClass
    {
        IAdapter Adapter { get ; }

        bool Restart();
    }
}
