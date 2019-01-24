using System;
using System.Collections.Generic;
using System.Text;

using PostSharp.Aspects ;
using PostSharp.Serialization ;

namespace Buttplug.Client.Platforms.Bluetooth.Aspects
{
    [PSerializable]
    public class NullDeadEndAttribute : MethodInterceptionAspect
    {
        /// <summary>
        ///     Starts at zero and helps determine number of accesses.
        /// </summary>
        // ReSharper disable once RedundantDefaultMemberInitializer - not obvious
        public int RoadBlock = 0 ;

        public override void OnInvoke ( MethodInterceptionArgs args )
        {
            //  Immediately increment so that a second thread cannot sneak by without touching.
            RoadBlock++ ;

            //  Potential miss but better to miss than fail in this one case.
            if ( RoadBlock > 1 )
            {
                args.ReturnValue = null ;
                return ;
            }

            //  Proceed with the original call this first time.
            args.Proceed();
        }
    }
}
