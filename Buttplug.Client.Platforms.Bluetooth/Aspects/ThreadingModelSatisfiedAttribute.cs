using System;
using System.Collections.Generic;
using System.Linq ;
using System.Reflection ;
using System.Text;

using JetBrains.Annotations ;

using PostSharp.Constraints ;
using PostSharp.Extensibility ;
using PostSharp.Patterns.Model ;

namespace Buttplug.Client.Platforms.Bluetooth.Aspects
{
    [UsedImplicitly]
    [MulticastAttributeUsage(MulticastTargets.Property | MulticastTargets.Field, Inheritance = MulticastInheritance.None)]
    public class ThreadingModelSatisfiedAttribute : ScalarConstraint
    {
        public override void ValidateCode(object target)
        {
            var targetType = ( Type ) target ;
            var attributes = targetType.CustomAttributes ;
            if ( MissingChildAttribute ( ref attributes )
              && MissingReferenceAttribute ( ref attributes )
              && MissingParentAttribute ( ref attributes ) )
                Message.Write (
                               targetType, 
                               SeverityType.Error,
                               "0",
                               "All properties and fields must be marked as a [Child], [Reference], or [Parent] to satisfy threading models using AggregatableAttribute.",
                               targetType.DeclaringType,
                               targetType.Name ) ;
        }

        private static bool MissingChildAttribute (ref IEnumerable <CustomAttributeData> attributes) => 
            attributes.All ( 
                            attribute => attribute.GetType () != typeof( ChildAttribute ) ) ;

        private static bool MissingReferenceAttribute(ref IEnumerable<CustomAttributeData> attributes) =>
            attributes.All(
                           attribute => attribute.GetType() != typeof( ReferenceAttribute ));

        private static bool MissingParentAttribute(ref IEnumerable<CustomAttributeData> attributes) =>
            attributes.All(
                           attribute => attribute.GetType() != typeof( ParentAttribute ));
    }
}
