using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection ;
using System.ComponentModel ;
using System.Linq ;
using System.Reflection ;
using System.Text;

using Buttplug.Client.Platforms.Bluetooth.Composition ;
using Buttplug.Client.Platforms.Bluetooth.Platforms ;

using Microsoft.Extensions.DependencyInjection ;
using Microsoft.Extensions.Options ;

using Newtonsoft.Json ;
using Newtonsoft.Json.Converters ;

namespace Buttplug.Client.Platforms.Bluetooth.Runtime
{
    [ EditorBrowsable ( EditorBrowsableState.Never ) ]
    public static class RuntimeExtensions
    {
        public static IServiceCollection AddMicroservices ( this IServiceCollection services, IEnumerable <IMicroService> micros )
        {
            foreach ( IMicroService microservice in micros )
            {
                services.AddSingleton <IMicroService> ( microservice ) ;
            }

            return services ;
        }

        public static string ToDebugString<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            return "{" + string.Join(",", dictionary.Select(kv => kv.Key + "=" + kv.Value).ToArray()) + "}";
        }
    }
}
