using System;
using System.Collections.Generic;
using System.Linq;

namespace Buttplug.Client.Platforms.Bluetooth.Attributes
{
    /// <summary>
    /// Indicates instances of the decorated type persist between usages
    /// </summary>
    [AttributeUsage( AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class DurableAttribute : Attribute
    {

    }
}
