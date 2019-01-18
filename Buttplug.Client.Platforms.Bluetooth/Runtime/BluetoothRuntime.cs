#region Header
// Buttplug.Client.Platforms.Bluetooth/Class1.cs - Created on 2019-01-16 at 8:35 PM by Sshado.
#endregion

using System ;
using System.Reflection ;
using System.Threading.Tasks ;

using Buttplug.Client.Platforms.Bluetooth.Platforms ;

using JetBrains.Annotations ;

using PostSharp.Patterns.Contracts ;

namespace Buttplug.Client.Platforms.Bluetooth
{
    public sealed class BluetoothRuntime
    {
        private static readonly string ApplicationUri ;

        public void Entry ()
        {

        }

        /// <summary>
        ///     Loads a reference to a desired type within the overall assembly.
        /// </summary>
        /// <param name="namespace">The specific namespace containing a type to be loaded.</param>
        /// <param name="className">The specific type to load from within the assembly.</param>
        /// <returns></returns>
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
    }
}
