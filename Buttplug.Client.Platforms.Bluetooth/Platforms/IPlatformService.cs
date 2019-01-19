#region Header
// Buttplug.Client.Platforms.Bluetooth/IPlatformService.cs - Created on 2019-01-16 at 8:32 PM by Sshado.
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JetBrains.Annotations;

namespace Buttplug.Client.Platforms.Bluetooth
{
    [PublicAPI]
    public interface IPlatformService
    {
        PlatformStatus State { get; }

        /// <summary>
        ///     Creates a new message with the default fields.
        /// </summary>
        /// <param name="topic">The topic or purpose of this message.</param>
        /// <returns>A new topic with properties .Topic, .Timestamp, and .Identity</returns>
        dynamic CreateMessage([NotNull] string topic);

        /// <summary>
        ///     Create a message with the default fields and return its .Identity property.
        /// </summary>
        /// <param name="topic">The topic or purpose of this message.</param>
        /// <returns>
        ///     A tuple containing a new message with the default fields, and
        ///     the value of .Identity so that it can be used without unboxing.
        /// </returns>
        (dynamic, Guid) CreateStampedMessage([NotNull] string topic);

        /// <summary>
        ///     Post a message for all Actors or Microservices.
        /// </summary>
        /// <param name="message">
        ///     A dynamic message with a string {.Topic} and a DateTime {.Timestamp}.
        /// </param>
        /// <returns>Task object.</returns>
        /* async */
        Task PostMessage([NotNull] dynamic message);

        /// <summary>
        ///     Called to terminate the system if a critical failure occurs in the complex actor state or platform-specific state.
        /// </summary>
        /// <param name="reason">As much detail as possible regarding the reason for fatality.</param>
        void Fatal(string reason);

        /// <summary>
        ///     Register an action to be performed if there are safe but critical steps to be taken to preserve data.
        /// </summary>
        /// <param name="cleanup">Strictly emergency cleanup measures in a non-functioning system.</param>
        void OnDeath([NotNull] Action cleanup);
    }
}
