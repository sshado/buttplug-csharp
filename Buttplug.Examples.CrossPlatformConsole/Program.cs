using System;

using Buttplug.Client.Platforms.Bluetooth ;
using Buttplug.Client.Platforms.Bluetooth.Runtime ;

namespace Buttplug.Examples.CrossPlatformConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var byte2 = byte.MaxValue;

            Console.WriteLine("Hello World!");
            var runtime = new BluetoothRuntime () ;
            runtime.Entry();
            Console.ReadLine () ;
        }
    }
}
