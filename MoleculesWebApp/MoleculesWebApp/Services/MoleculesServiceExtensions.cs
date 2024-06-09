using Molecules.Core;
using Molecules.Core.Data;
using Molecules.Logger;
using Molecules.Core.Validation;

namespace MoleculesWebApp.Services
{
    public static class MoleculesServiceExtensions
    {
        /// <summary>
        /// Add all services and middleware for Molecules API
        /// </summary>
        /// <param name="services">The application Services Collection</param>
        /// <param name="configuration">The configurations connected to logging</param>
        /// <returns>The modified services collection</returns>
        public static IServiceCollection AddMoleculesServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddCoreServices(ServiceLifetime.Scoped)
                                .AddRepositoryServices(ServiceLifetime.Scoped)
                                .AddValidationServices(ServiceLifetime.Scoped)
                                .AddLoggerServices(ServiceLifetime.Scoped, configuration.GetSection("MoleculesLoggerOptions"));
        }
    }
}
