#region Header
// Buttplug.Client.Platforms.Bluetooth/Class1.cs - Created on 2019-01-16 at 8:35 PM by Sshado.
#endregion

using System ;
using System.IO ;
using System.Reflection ;

using Buttplug.Client.Platforms.Bluetooth.Aspects ;
using Buttplug.Client.Platforms.Bluetooth.Platforms ;

using JetBrains.Annotations ;

using PostSharp ;
using PostSharp.Patterns.Diagnostics ;
using PostSharp.Patterns.Diagnostics.Backends.Console ;
using PostSharp.Patterns.Model ;
using PostSharp.Patterns.Threading ;

using Serilog ;

using NotNullAttribute = JetBrains.Annotations.NotNullAttribute;

namespace Buttplug.Client.Platforms.Bluetooth.Runtime
{
    /// <summary>
    ///     Represents a runtime which is statically constructed and frozen upon entry.
    /// </summary>
    [ LoggingExceptionsHandled ]
    [ ThreadingModelSatisfied ]
    [ Freezable ]
    public sealed class BluetoothRuntime
    {
        static BluetoothRuntime ()
        {
            Platform = new CommonPlatform() ; 
            Assembly = GetAssembly ( typeof( BluetoothRuntime ) ) ;
            ApplicationUri = Path.GetDirectoryName (Assembly.Location ) ;
        }

        [ Pure ] private static Assembly GetAssembly<T>(T type) => type.GetType ().GetTypeInfo ().Assembly ;

        [ NotNull ] private static readonly Assembly Assembly ; 
        [ NotNull ] private static readonly string ApplicationUri ;
        [ NotNull , Child ] private static readonly CommonPlatform Platform ;
        [ NotNull, Reference ] private readonly ILogger _log = Log.ForContext<BluetoothRuntime>();


        [ EntryPoint ]
        public void Entry ()
        {
            LoggingServices.DefaultBackend = new ConsoleLoggingBackend();
            try
            {
                Post.Cast<BluetoothRuntime, IFreezable>(this).Freeze();
                Post.Cast<CommonPlatform, IFreezable>(Platform).Freeze();

                var cleanlyLaunch = Platform.BluetoothHost.Start (Platform) ;
                var returned = cleanlyLaunch.ContinueWith ( ( t ) =>
                                                            {
                                                                if ( t.IsFaulted || t.IsCanceled )
                                                                    _log
                                                                       .Fatal ( $"Fatal exception in the common platform at runtime." +
                                                                                $"{Environment.NewLine}Stack: -------------"          +
                                                                                $"{Environment.NewLine}{t.Exception.ToString ()}" ) ;
                                                                else
                                                                    _log
                                                                       .Information ( "Successfully launched the common platform from the bluetooth runtime." ) ;
                                                            } ) ;
                returned.Wait () ;
            }
            catch ( ObjectReadOnlyException roEx )
            {
                _log.Fatal ( $"Property or state were attempted on a frozen object." +
                             $"${Environment.NewLine}BluetoothRuntime Freeze(): {roEx.Data}" ) ;
                Platform.Crash ( "Fatal attempt to modify a frozen object." ) ;
            }
            catch ( ThreadMismatchException threadEx )
            {
                _log.Fatal ( $"An object was accessed from a different thread than it was created." +
                             $"{Environment.NewLine}Please use a threading model or freeze the object before crossing boundaries." +
                             $"{threadEx}" ) ;
                Platform.Crash ( "Fatal thread access exception was thrown." ) ;
            }
            catch ( Exception ex )
            {
                _log.Fatal($"Could not freeze and launch the common platform from the bluetooth runtime." +
                           $"{Environment.NewLine}{ex}");
                Platform.Crash("Unknown failure occurred in an EntryPointAttribute for the bluetooth runtime.");
            }
        }

        
    }
}
