#region Header
// Buttplug.Client.Platforms.Bluetooth/Class1.cs - Created on 2019-01-16 at 8:35 PM by Sshado.
#endregion

using System ;
using System.IO ;
using System.Reflection ;
using System.Threading.Tasks ;

using Buttplug.Client.Platforms.Bluetooth.Aspects ;
using Buttplug.Client.Platforms.Bluetooth.Platforms ;

using JetBrains.Annotations ;

using PostSharp.Patterns.Contracts ;
using PostSharp.Patterns.Diagnostics ;
using PostSharp.Patterns.Diagnostics.Backends.Console ;
using PostSharp.Patterns.Model ;
using PostSharp.Patterns.Threading ;

using NotNullAttribute = JetBrains.Annotations.NotNullAttribute;

namespace Buttplug.Client.Platforms.Bluetooth
{
    [ LoggingExceptionsHandled ]
    public sealed class BluetoothRuntime
    {
        static BluetoothRuntime ()
        {
            Platform = new CommonPlatform();
            Assembly = GetAssembly ( typeof( BluetoothRuntime ) ) ;
            ApplicationUri = Path.GetDirectoryName (Assembly.Location ) ;
        }

        [ Pure ] private static Assembly GetAssembly<T>(T type) => type.GetType ().GetTypeInfo ().Assembly ;

        [ NotNull ] private static readonly Assembly Assembly ; 
        [ NotNull ] private static readonly string ApplicationUri ;
        [ NotNull , Child ] private static readonly CommonPlatform Platform ;

        [ EntryPoint ]
        public void Entry ()
        {
            LoggingServices.DefaultBackend = new ConsoleLoggingBackend();
        }
    }
}
