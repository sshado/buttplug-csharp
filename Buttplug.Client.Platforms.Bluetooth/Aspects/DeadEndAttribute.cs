#region Header
// Buttplug.Client.Platforms.Bluetooth/DeadEndAttribute.cs - Created on 2019-01-23 at 8:50 PM by Sshado.
// This file is part of Buttplug.io which is BSD-3 licensed.
#endregion

#region Using
using PostSharp.Aspects ;
using PostSharp.Serialization ;
#endregion

namespace Buttplug.Client.Platforms.Bluetooth.Aspects
{
    [ PSerializable ]
    public class NullDeadEndAttribute : MethodInterceptionAspect
    {
        #region Properties & Fields
        /// <summary>
        ///     Starts at zero and helps determine number of accesses.
        /// </summary>

        // ReSharper disable once RedundantDefaultMemberInitializer - not obvious
        public int RoadBlock = 0 ;
        #endregion

        #region Members
        public override void OnInvoke ( MethodInterceptionArgs args )
        {
            //  Immediately increment so that a second thread cannot sneak by without touching.
            RoadBlock ++ ;

            //  Potential miss but better to miss than fail in this one case.
            if ( RoadBlock > 1 )
            {
                args.ReturnValue = null ;
                return ;
            }

            //  Proceed with the original call this first time.
            args.Proceed () ;
        }
        #endregion
    }
}
