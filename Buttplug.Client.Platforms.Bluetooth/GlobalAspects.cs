using System ;

using Buttplug.Client.Platforms.Bluetooth.Aspects ;

using PostSharp.Patterns.Diagnostics;
using PostSharp.Extensibility;

[assembly: Log(AttributePriority = 1,
    AttributeTargetMemberAttributes =
        MulticastAttributes.Public | MulticastAttributes.Private | MulticastAttributes.Internal | MulticastAttributes.Protected)]

//[assembly: Log(AttributePriority = 1, 
//    AttributeTargetMemberAttributes = 
//        MulticastAttributes.Protected | MulticastAttributes.Internal | MulticastAttributes.Public)]

[assembly: Log(
    AttributePriority = 2, 
    AttributeExclude = true, 
    AttributeTargetMembers = "get_*")]

//[assembly: ThreadingModelSatisfied( AttributePriority = 3, AttributeTargetTypes= "Buttplug.Client.Platforms.Bluetooth.*", AttributeTargetMemberAttributes = MulticastAttributes.Public | MulticastAttributes.Internal)]
//[assembly: ThreadingModelSatisfied( AttributePriority = 4, AttributeExclude = true, AttributeTargetTypes= "ActorIdentity")]

