using System ;
using System.Collections.Generic ;
using System.Diagnostics ;
using System.Linq ;
using System.Reflection ;

using Buttplug.Client.Platforms.Bluetooth.Aspects ;

using JetBrains.Annotations ;

using Microsoft.DotNet.PlatformAbstractions ;
using Microsoft.Extensions.DependencyModel ;

using PostSharp.Patterns.Contracts ;
using PostSharp.Patterns.Model ;
using PostSharp.Patterns.Threading ;

using Serilog ;

namespace Buttplug.Client.Platforms.Bluetooth.Platforms
{
    [ Freezable ]
    [ ThreadingModelSatisfied ]
    public class CommonPlatform
    {
        private Assembly _caller () => Assembly.GetCallingAssembly () ;
        private Assembly _entry ()  => Assembly.GetEntryAssembly () ;

        [ Reference ] public CommonHost BluetoothHost = new CommonHost () ;

        /// <summary>
        ///     Locates all matching types in assemblies which were known at compile time.
        /// </summary>
        /// <typeparam name="T">A type known at compile time.</typeparam>
        /// <remarks>
        ///     This should not be used for dynamically compiled assemblies.
        /// </remarks>
        [ Pure ]
        public static IEnumerable <Type> GetAllTypesOf <T> ()
        {
            var platform             = Environment.OSVersion.Platform.ToString () ;
            var runtimeAssemblyNames = DependencyContext.Default.GetRuntimeAssemblyNames ( platform ) ;

            return runtimeAssemblyNames
                  .Select ( Assembly.Load )
                  .SelectMany ( a => a.ExportedTypes )
                  .Where ( t => typeof( T ).IsAssignableFrom ( t ) ) ;
        }

        /// <summary>
        ///     Return types which are dynamically known from the entry-point assembly.
        /// </summary>
        [ Pure ]
        private IEnumerable <Type> FindEntryTypes <T> ()
        {
            _entry ().GetReferencedAssemblies () ;
            foreach ( TypeInfo type in _entry ().DefinedTypes )
                if ( type.ImplementedInterfaces.Contains ( typeof( IMicroService ) ) )
                    yield return type ;
        }

        /// <summary>
        ///     Validate whether a namespace exists in an assembly.
        /// </summary>
        /// <param name="assembly">A reference to an assembly.</param>
        /// <param name="namespace">The expected namespace.</param>
        [ Pure ]
        private bool NamespaceExists ( [ JetBrains.Annotations.NotNull ] [ PostSharp.Patterns.Contracts.NotNull ]
                                       Assembly assembly,
                                       [ JetBrains.Annotations.NotNull ] [ Required ]
                                       string @namespace )
        {
            return assembly.GetTypes ().Any ( type => type.Namespace == @namespace ) ;
        }

        /// <summary>
        ///     Load a reference to a type and perform all necessary validation.
        /// </summary>
        /// <param name="namespace">The name of a specific namespace.</param>
        /// <param name="className">The name of a specific type to load.</param>
        /// <returns>
        ///     A class which you can start using immediately.
        /// </returns>
        /// <remarks>
        ///     This method currently only supports classes contained within the calling assembly.
        ///     The class must exist within a namespace ending with its <see cref="Platform"/>.
        ///     e.g.    Windows == Win32NT == MyAssembly.MyType.Platforms.Win32NT
        ///     e.g.    (INativeType) CommonPlatform.GetPlatformClass( "MyAssembly.MyType.Platforms", "ConcreteNativeType" );
        /// </remarks>
        [ Pure ]
        public IPlatformClass GetPlatformClass (
            [ JetBrains.Annotations.NotNull ] [ Required ]
            string @namespace,
            [ JetBrains.Annotations.NotNull ] [ Required ]
            string className )
        {
            // Get calling assembly.
            var calling = _caller () ;

            return this.GetPlatformClass ( calling, @namespace, className ) ;
        }

        [ Pure ]
        private IPlatformClass GetPlatformClass (
            [ JetBrains.Annotations.NotNull ] [ PostSharp.Patterns.Contracts.NotNull ]
            Assembly assembly,
            [ JetBrains.Annotations.NotNull ] [ Required ]
            string @namespace,
            [ JetBrains.Annotations.NotNull ] [ Required ]
            string className )
        {
            // Validate base namespace.
            if ( ! this.NamespaceExists ( assembly, @namespace ) )
                this.Crash ( $"Namespace not be located: {@namespace} in assembly: {assembly.FullName}" ) ;

            // Get platform-specific namespace.
            string platformNamespace = $"{@namespace}.{Environment.OSVersion.Platform.ToString ()}" ;

            // Validate platform-specific namespace.
            if ( ! this.NamespaceExists ( assembly, platformNamespace ) )
                this.Crash ( $"CommonPlatform: {Environment.OSVersion.Platform.ToString ()} was not found in {@namespace}" ) ;

            // Create platform-specific class instance.
            var typeName = $"{platformNamespace}.{className}" ;

            try
            {
                var type = assembly.GetType ( typeName ) ;

                var instance = ( IPlatformClass ) Activator.CreateInstance ( type ) ;

                return instance ;
            }
            catch ( Exception ex )
            {
                this.Crash ( $"Failed to create a native platform class called {typeName}: {ex.Message}" ) ;

                // ReSharper disable once HeuristicUnreachableCode - satisfy verifier
                return null ;
            }
        }

        /// <summary>
        ///     A critical problem exists and evokes a last-second cleanup before halting.
        /// </summary>
        /// <param name="reason">A detailed reason for the incoming crash.</param>
        [ Pure, ContractAnnotation ( "=> halt" ) ]
        public void Crash ( string reason = "A catastrophic failure has occurred." )
        {
            Log.Fatal ( ( reason ) ) ;

            // Check for the presence of debugger and yield.
            this.EnterDebugger () ;

            // ReSharper disable once InconsistentNaming - 128 (0x80) indicates no need to wait for child processes
            const int ERROR_WAIT_NO_CHILDREN = 128 ;
            Environment.Exit ( ERROR_WAIT_NO_CHILDREN ) ;
        }

        [ Pure, Conditional ( "DEBUG" ) ]
        private void EnterDebugger ()
        {
            if ( Debugger.IsAttached )
                Debugger.Break () ;
        }
    }
}
