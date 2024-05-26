using Microsoft.Extensions.DependencyInjection;
using Molecules.Logger.Logger;
using Molecules.Shared.Logger;

namespace Molecules.Logger
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddLoggerServices(this IServiceCollection services, ServiceLifetime serviceLifetime)
        {
            switch (serviceLifetime)
            {
                case ServiceLifetime.Singleton:
                    services.AddSingleton<IMoleculesLogger, MoleculesLogger>();

                    break;
                case ServiceLifetime.Scoped:
                    services.AddScoped<IMoleculesLogger, MoleculesLogger>();


                    break;
                case ServiceLifetime.Transient:
                    services.AddTransient<IMoleculesLogger, MoleculesLogger>();
                    break;
            }

            return services;
        }
    }
}
