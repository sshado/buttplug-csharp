using System;
using System.Collections.Generic;
using System.Linq ;
using System.Reflection ;
using System.Text;

using JetBrains.Annotations ;

using PostSharp.Patterns.Contracts ;

namespace Buttplug.Client.Platforms.Bluetooth.Platforms
{
    class CommonPlatform
    {
        /// <summary>
        ///     Validate whether a namespace exists in an assembly.
        /// </summary>
        /// <param name="assembly">A reference to an assembly.</param>
        /// <param name="namespace">The expected namespace.</param>
        [Pure]
        private bool DoesNamespaceExist([JetBrains.Annotations.NotNull] [PostSharp.Patterns.Contracts.NotNull]
                                        Assembly assembly,
                                        [JetBrains.Annotations.NotNull] [Required]
                                        string @namespace)
        {
            return assembly.GetTypes().Any(type => type.Namespace == @namespace);
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
        ///     The class must exist within a namespace ending with its <see cref="Environment.OSVersion.Platform"/>.
        ///     e.g.    Windows == Win32NT == MyAssembly.MyType.Platforms.Win32NT
        ///     e.g.    (INativeType) CommonPlatform.GetPlatformClass( "MyAssembly.MyType.Platforms", "ConcreteNativeType" );
        /// </remarks>
        [Pure]
        public IPlatformClass GetPlatformClass(
            [JetBrains.Annotations.NotNull] [Required]
            string @namespace,
            [JetBrains.Annotations.NotNull] [Required]
            string className)
        {
            // Get calling assembly.
            var calling = Assembly.GetCallingAssembly();

            return this.GetPlatformClass(calling, @namespace, className);
        }

        [Pure]
        private IPlatformClass GetPlatformClass([JetBrains.Annotations.NotNull] [PostSharp.Patterns.Contracts.NotNull]
                                                Assembly assembly,
                                                [JetBrains.Annotations.NotNull] [Required]
                                                string @namespace,
                                                [JetBrains.Annotations.NotNull] [Required]
                                                string className)
        {
            // Validate base namespace.
            if (!this.DoesNamespaceExist(assembly, @namespace))
                this.Die($"Base namespace does not exist: {@namespace}");

            // Get platform-specific namespace.
            string platformNamespace = $"{@namespace}.{Environment.OSVersion.Platform.ToString()}";

            // Validate platform-specific namespace.
            if (!this.DoesNamespaceExist(assembly, platformNamespace))
                this.Die($"Platform: {Environment.OSVersion.Platform.ToString()} not supported for namespace {@namespace}");

            // Create platform-specific class instance.
            var typeName = $"{platformNamespace}.{className}";

            try
            {
                var type = assembly.GetType(typeName);

                var instance = (IPlatformClass)Activator.CreateInstance(type);

                return instance;
            }
            catch (Exception ex)
            {
                this.Die($"Failed to create platform-specific class {typeName}: {ex.Message}");

                // ReSharper disable once HeuristicUnreachableCode - exists to satisfy verifier
                return null;
            }
        }
    }
}
