#region Header
// Buttplug.Client.Platforms.Bluetooth/LoggingExceptionsHandledAttribute.cs - Created on 2019-01-17 at 10:41 PM by Sshado.
// This file is part of Buttplug.io which is BSD-3 licensed.
#endregion

#region Using
using System ;

using PostSharp.Aspects ;
using PostSharp.Patterns.Diagnostics ;
using PostSharp.Patterns.Diagnostics.Contexts ;
using PostSharp.Serialization ;
#endregion

namespace Buttplug.Client.Platforms.Bluetooth.Aspects
{
    /// <summary>
    ///     Wraps a logging exception handler around <see cref="LoggingServices" /> implementations.
    /// </summary>
    [ PSerializable ]
    internal class LoggingExceptionsHandledAttribute : OnMethodBoundaryAspect, ILoggingExceptionHandler
    {
        #region Members
        public static void Initialize ()
        {
            LoggingServices.ExceptionHandler = new ThrowLoggingExceptionHandler () ;
        }

        public override void OnEntry ( MethodExecutionArgs args )
        {
            Initialize () ;
        }
        #endregion

        #region Types & Implementations
        public void OnInternalException ( LoggingExceptionInfo exceptionInfo )
        {
            throw new Exception ( "Internal logging failed with exception.", exceptionInfo.Exception ) ;
        }

        public void OnInvalidUserCode ( ref    CallerInfo callerInfo, LoggingTypeSource source, string message,
                                        params object[]   args )
        {
            throw new InvalidOperationException ( string.Format ( message, args ) ) ;
        }
        #endregion
    }
}
