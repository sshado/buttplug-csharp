using System;
using System.Collections.Generic;
using System.Linq ;
using System.Text;
using System.Threading.Tasks ;

using Buttplug.Client.Platforms.Bluetooth.Actors ;
using Buttplug.Client.Platforms.Bluetooth.Aspects ;
using Buttplug.Client.Platforms.Bluetooth.Exceptions ;
using Buttplug.Client.Platforms.Bluetooth.Runtime ;

using JetBrains.Annotations ;

using Microsoft.Extensions.Hosting ;
using Microsoft.Extensions.Configuration.CommandLine ;
using Microsoft.Extensions.Configuration ;
using Microsoft.Extensions.Configuration.Json ;
using Microsoft.Extensions.DependencyInjection ;
using Microsoft.Extensions.Logging ;

using PostSharp.Patterns.Model ;
using PostSharp.Patterns.Threading ;
using PostSharp.Serialization ;

using Serilog ;
using Serilog.Events ;

using ILogger = Serilog.ILogger ;

namespace Buttplug.Client.Platforms.Bluetooth.Platforms
{
    [ PrivateThreadAware ]
    [ ThreadingModelSatisfied ]
    public class CommonHost
    {
        [Reference]
        private readonly ILogger _log = Log.ForContext <CommonHost>() ;

        [Child]
        public IEnumerable <IMicroService> Microservices { get ; private set ; }

        [ Reentrant ]
        public async Task Start ( CommonPlatform platform, [ CanBeNull ] string[] args = null )
        {
            var developing = BeingDeveloped () ;

            var builder = new HostBuilder ()
                         .ConfigureAppConfiguration ( ( context, config ) =>
                                                      {
                                                          config.AddJsonFile ( "appsettings.json", optional: true ) ;
                                                          config.AddEnvironmentVariables () ;

                                                          if ( args != null )
                                                              config.AddCommandLine ( args ) ;

                                                          //if ( developing )
                                                          //    config.AddUserSecrets <CommonHost> () ;

                                                      } )
                         .UseSerilog ( ( host, log ) =>
                                       {
                                           bool configured =
                                               Enum.TryParse (typeof(Serilog.Events.LogEventLevel),
                                                              host.Configuration.GetSection ( "LoggingLevel" ).Value,
                                                              true,
                                                              out object config ) ;
                                           if ( developing && configured )
                                               log.WriteTo.Console ().MinimumLevel
                                                  .Is ( ( LogEventLevel ) config ) ;
                                           else
                                               log.WriteTo.Console ().MinimumLevel
                                                  .Is ( LogEventLevel.Verbose ) ;
                                       } )
                         .ConfigureServices ( configureDelegate: ( context, services ) =>
                                                                 {
                                                                     services.AddOptions () ;
                                                                     services.Configure <AppConfig> ( context
                                                                                                     .Configuration
                                                                                                     .GetSection ( "AppConfig" ) ) ;

                                                                     services
                                                                        .AddSingleton <IBluetoothHost, BluetoothHost> () ;

                                                                     var provider = services.BuildServiceProvider();
                                                                     var bleHost =
                                                                         provider.GetService <IBluetoothHost> () ;
                                                                     bleHost.Initialize ( platform ) ;

                                                                     //var microservices =
                                                                     //    (IEnumerable<IMicroService>) CommonPlatform
                                                                     //       .GetAllTypesOf <IMicroService> () ?? throw new FailedToLocateMicroservicesException("CommonPlatform:\r\n  CommonPlatform.GetAllTypesOf<IMicroService> failed to locate any services." );

                                                                     //services.AddMicroservices ( microservices ) ;
                                                                 } ) ;
            await builder.RunConsoleAsync () ;
        }

        private bool BeingDeveloped ()
        {
            var env =
                Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");

            return string.IsNullOrEmpty(env) ||
                              env.Equals("Development", StringComparison.Ordinal);
        }
    }
}
