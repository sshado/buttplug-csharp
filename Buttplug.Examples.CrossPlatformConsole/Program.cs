using System;

using Buttplug.Client.Platforms.Bluetooth ;

namespace Buttplug.Examples.CrossPlatformConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var runtime = new BluetoothRuntime () ;
            runtime.Entry();
        }
    }
}
