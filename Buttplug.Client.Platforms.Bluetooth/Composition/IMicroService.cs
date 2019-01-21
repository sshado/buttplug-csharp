#region Header
// Buttplug.Client.Platforms.Bluetooth/IPlatformService.cs - Created on 2019-01-16 at 8:32 PM by Sshado.
#endregion

using System.Threading.Tasks;

using Buttplug.Client.Platforms.Bluetooth.Composition ;

using JetBrains.Annotations;

namespace Buttplug.Client.Platforms.Bluetooth
{
    /// <summary>
    ///     The interface presented by a microservice which the platform service uses to import in Managed Extensibility Framework.
    /// </summary>
    public interface IMicroService
    {
        Task<bool> Initialize([NotNull] IPlatformService coreService);

        [ NotNull ]
        ActorIdentity Identity { get ; }

        /* async */
        [ NotNull ]
        Task HandleMessage([NotNull] dynamic message);
    }
}
