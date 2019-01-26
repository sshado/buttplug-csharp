#region Header
// Buttplug.Client.Platforms.Bluetooth/DurableAttribute.cs - Created on 2019-01-16 at 9:19 PM by Sshado.
// This file is part of Buttplug.io which is BSD-3 licensed.
#endregion

#region Using
using System ;
#endregion

namespace Buttplug.Client.Platforms.Bluetooth.Attributes
{
    /// <summary>
    ///     Indicates instances of the decorated type persist between usages
    /// </summary>
    [ AttributeUsage ( AttributeTargets.Class ) ]
    public sealed class DurableAttribute : Attribute { }
}
