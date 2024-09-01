using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Molecules.Core;
using Molecules.Core.Data;
using Molecules.Core.Validation;
using Molecules.Logger;
using Molecules.services;

namespace Molecules
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// Add all services and middleware for Molecules API
        /// </summary>
        /// <param name="services">The application Services Collection</param>
        /// <returns>The modified services collection</returns>
        public static IServiceCollection AddMoleculesServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddApps()
                             .AddLoggerServices(ServiceLifetime.Transient, configuration.GetSection("MoleculesLoggerOptions"))
                             .AddRepositoryServices(ServiceLifetime.Transient)
                             .AddCoreServices(ServiceLifetime.Transient)
                             .AddValidationServices(ServiceLifetime.Transient);
        }

        internal static IServiceCollection AddApps(this IServiceCollection services)
        {
            services.AddSingleton<CalcDeliveryServices>();
            services.AddSingleton<MoleculeReportService>();
            return services;
        }


    }
}
