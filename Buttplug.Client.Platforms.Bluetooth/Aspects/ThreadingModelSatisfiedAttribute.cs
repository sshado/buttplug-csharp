using System;
using System.Collections.Generic;
using System.Linq ;
using System.Reflection ;
using System.Text;

using JetBrains.Annotations ;

using PostSharp.Constraints ;
using PostSharp.Extensibility ;
using PostSharp.Patterns.Model ;
using PostSharp.Reflection ;
using PostSharp.Serialization ;

namespace Buttplug.Client.Platforms.Bluetooth.Aspects
{
    [MulticastAttributeUsage(MulticastTargets.Property, TargetMemberAttributes = MulticastAttributes.Public | MulticastAttributes.Internal)]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class ThreadingModelSatisfiedAttribute : ScalarConstraint
    {
        public override void ValidateCode(object target)
        {
            var missingChildAttribute = ReflectionSearch.HasCustomAttribute(target, typeof(ChildAttribute));
            var missingReferenceAttribute = ReflectionSearch.HasCustomAttribute(target, typeof(ReferenceAttribute));
            var missingParentAttribute = ReflectionSearch.HasCustomAttribute(target, typeof(ParentAttribute));

            var attributes = ReflectionSearch.GetCustomAttributesOnTarget ( target ) ;

           // foreach (var ((MethodInfo) method) in ((PropertyInfo) target))
           // var name =

            if ( !missingChildAttribute
              && !missingReferenceAttribute
              && !missingParentAttribute )
                Message.Write(
                               MessageLocation.Of(target),
                               SeverityType.Error,
                               "BP000",
                               "[Type: {0}] All properties and fields must be marked as a [Child], [Reference], or [Parent] to satisfy threading models using AggregatableAttribute.",
                               target.GetType().Name + attributes.Select ( x => x.Attribute.TypeId ));
        }

    }
}
