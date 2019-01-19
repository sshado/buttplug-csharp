using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks ;

using Buttplug.Client.Platforms.Bluetooth.Runtime ;

using JetBrains.Annotations ;

using Microsoft.Extensions.Hosting ;
using Microsoft.Extensions.Configuration.CommandLine ;
using Microsoft.Extensions.Configuration ;
using Microsoft.Extensions.Configuration.Json ;
using Microsoft.Extensions.DependencyInjection ;
using Microsoft.Extensions.Logging ;

using Serilog ;

using ILogger = Serilog.ILogger ;

namespace Buttplug.Client.Platforms.Bluetooth.Platforms
{
    internal class CommonHost
    {
        private readonly ILogger _log = Log.ForContext <CommonHost>() ;
        public IEnumerable <IMicroService> Microservices { get ; private set ; }

        public async Task Start ([CanBeNull]string[] args, CommonPlatform platform)
        {
            var developing = await BeingDeveloped () ;

            var builder = new HostBuilder ()
                         .ConfigureAppConfiguration ( ( context, config ) =>
                                                      {
                                                          config.AddJsonFile ( "appsettings.json", optional: true ) ;
                                                          config.AddEnvironmentVariables () ;

                                                          if ( args != null )
                                                              config.AddCommandLine ( args ) ;

                                                          if ( developing )
                                                              config.AddUserSecrets <CommonHost> () ;

                                                      } )
                         .UseSerilog ( ( host, log ) =>
                                       {
                                           if ( developing )
                                               log.WriteTo.Console ().MinimumLevel
                                                  .Is ( ( Serilog.Events.LogEventLevel.Verbose ) ) ;
                                       } )
                         .ConfigureServices ( configureDelegate: ( context, services ) =>
                                                                 {
                                                                     services.AddOptions () ;
                                                                     services.Configure <AppConfig> ( context.Configuration.GetSection ( "Logging" ) ) ;
                                                                     services.AddMicroservices ( platform ) ;
                                                                 } ) ;
        }

        private async Task <bool> BeingDeveloped ()
        {
            var env =
                Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");

            return string.IsNullOrEmpty(env) ||
                              env.Equals("Development", StringComparison.Ordinal);
        }
    }
}
