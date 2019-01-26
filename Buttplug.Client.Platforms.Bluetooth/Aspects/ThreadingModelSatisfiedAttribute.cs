#region Header
// Buttplug.Client.Platforms.Bluetooth/ThreadingModelSatisfiedAttribute.cs - Created on 2019-01-20 at 12:49 AM by Sshado.
// This file is part of Buttplug.io which is BSD-3 licensed.
#endregion

#region Using
using System ;
using System.Linq ;

using PostSharp.Constraints ;
using PostSharp.Extensibility ;
using PostSharp.Patterns.Model ;
using PostSharp.Reflection ;
#endregion

namespace Buttplug.Client.Platforms.Bluetooth.Aspects
{
    [ MulticastAttributeUsage ( MulticastTargets.Property, TargetMemberAttributes =
        MulticastAttributes.Public | MulticastAttributes.Internal ) ]
    [ AttributeUsage ( AttributeTargets.All, AllowMultiple = true ) ]
    public class ThreadingModelSatisfiedAttribute : ScalarConstraint
    {
        #region Members
        public override void ValidateCode ( object target )
        {
            var hasChildAttribute     = ReflectionSearch.HasCustomAttribute ( target, typeof( ChildAttribute ) ) ;
            var hasReferenceAttribute = ReflectionSearch.HasCustomAttribute ( target, typeof( ReferenceAttribute ) ) ;
            var hasParentAttribute    = ReflectionSearch.HasCustomAttribute ( target, typeof( ParentAttribute ) ) ;

            var attributes = ReflectionSearch.GetCustomAttributesOnTarget ( target ) ;

            // foreach (var ((MethodInfo) method) in ((PropertyInfo) target))
            // var name =

            if ( ! hasChildAttribute
              && ! hasReferenceAttribute
              && ! hasParentAttribute )
                Message.Write (
                               MessageLocation.Of ( target ),
                               SeverityType.Error,
                               "BP000",
                               "[Type: {0}] All properties and fields must be marked as a [Child], [Reference], or [Parent] to satisfy threading models using AggregatableAttribute.",
                               target.GetType ().Name + attributes.Select ( x => x.Attribute.TypeId ) ) ;
        }
        #endregion
    }
}
