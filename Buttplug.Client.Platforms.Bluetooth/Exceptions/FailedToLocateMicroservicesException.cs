using System;
using System.Collections.Generic;
using System.Text;

namespace Buttplug.Client.Platforms.Bluetooth.Exceptions
{
    public class FailedToLocateMicroservicesException : Exception
    {
        public FailedToLocateMicroservicesException(string message) : base(message)
        {
        }
    }
}
