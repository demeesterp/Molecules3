using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Molecules.Core.Data;
using Molecules.extensions;

namespace Molecules
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args)
                    .ConfigureServices((hostContext, services) => {

                        services.AddMoleculesServices(hostContext.Configuration);
                        services.AddDbContext<MoleculesDbContext>(
                                     options => options.UseNpgsql(hostContext.Configuration.GetConnectionString("ConnectionString")),
                                                                    ServiceLifetime.Transient,
                                                                      ServiceLifetime.Transient);

                        // Start the main application
                        services.AddHostedService<MoleculesApp>();
                    });

            builder.ConfigureAppConfiguration((hostContext, options) => {
                options.AddEnvironmentVariables();
                options.AddCommandLine(args);
                options.AddJsonFile("appsettings.json", optional: false);
                options.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: false);
            });

            builder.Build().Run();

        }
    }
}
