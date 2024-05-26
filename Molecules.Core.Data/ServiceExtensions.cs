using Microsoft.Extensions.DependencyInjection;
using Molecules.Core.Data.Factories;
using Molecules.Core.Data.Repositories;
using Molecules.Core.Factories.Reports;
using Molecules.Core.Repositories;

namespace Molecules.Core.Data
{
    public static  class ServiceExtensions
    {

        public static IServiceCollection AddRepositoryServices(this IServiceCollection services, ServiceLifetime serviceLifetime)
        {
            switch (serviceLifetime)
            {
                case ServiceLifetime.Singleton:
                    services.AddSingleton<ICalcOrderFactory, CalcOrderFactory>();
                    services.AddSingleton<ICalcOrderRepository, CalcOrderRepository>();
                    services.AddSingleton<ICalcOrderItemRepository, CalcOrderItemRepository>();
                    services.AddSingleton<IMoleculeRepository, MoleculeRepository>();

                    services.AddSingleton<ICalcOrderItemFactory, CalcOrderItemFactory>();
                    services.AddSingleton<ICalcMoleculeFactory, CalcMoleculeFactory>();
                    services.AddSingleton<IMoleculeReportFactory, MoleculeReportFactory>();

                    break;
                case ServiceLifetime.Scoped:
                    services.AddScoped<ICalcOrderFactory, CalcOrderFactory>();
                    services.AddScoped<ICalcOrderRepository, CalcOrderRepository>();
                    services.AddScoped<ICalcOrderItemRepository, CalcOrderItemRepository>();
                    services.AddScoped<IMoleculeRepository, MoleculeRepository>();

                    services.AddScoped<ICalcOrderItemFactory, CalcOrderItemFactory>();
                    services.AddScoped<ICalcMoleculeFactory, CalcMoleculeFactory>();
                    services.AddScoped<IMoleculeReportFactory, MoleculeReportFactory>();

                    break;
                case ServiceLifetime.Transient:
                    services.AddTransient<ICalcOrderFactory, CalcOrderFactory>();
                    services.AddTransient<ICalcOrderRepository, CalcOrderRepository>();
                    services.AddTransient<ICalcOrderItemRepository, CalcOrderItemRepository>();
                    services.AddTransient<IMoleculeRepository, MoleculeRepository>();

                    services.AddTransient<ICalcOrderItemFactory, CalcOrderItemFactory>();
                    services.AddTransient<ICalcMoleculeFactory, CalcMoleculeFactory>();
                    services.AddTransient<IMoleculeReportFactory, MoleculeReportFactory>();
                    break;
            }

            return services;
        }

    }
}
