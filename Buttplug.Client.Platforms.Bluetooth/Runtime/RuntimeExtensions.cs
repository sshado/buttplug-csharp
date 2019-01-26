using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection ;
using System.ComponentModel ;
using System.Linq ;

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
