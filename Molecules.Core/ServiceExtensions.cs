using Microsoft.Extensions.DependencyInjection;
using Molecules.Core.Factories.CalcInput;
using Molecules.Core.Factories.Molecules;
using Molecules.Core.Services.Analysis;
using Molecules.Core.Services.CalcDelivery;
using Molecules.Core.Services.CalcMolecules;
using Molecules.Core.Services.CalcOrders;
using Molecules.Core.Services.Reports;

namespace Molecules.Core
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services, ServiceLifetime serviceLifetime)
        {
            switch (serviceLifetime)
            {
                case ServiceLifetime.Singleton:
                    services.AddSingleton<ICalcOrderService, CalcOrderService>();
                    services.AddSingleton<ICalcOrderItemService, CalcOrderItemService>();
                    services.AddSingleton<ICalcMoleculeService, CalcMoleculeService>();
                    services.AddSingleton<ICalcDeliveryService, CalcDeliveryService>();
                    services.AddSingleton<IMoleculeReportService, MoleculeReportService>();
                    services.AddSingleton<IMoleculeAnalysisService, MoleculeAnalysisService>();


                    services.AddSingleton<IGmsCalcInputFactory, GmsCalcInputFactory>();
                    services.AddSingleton<IMoleculeFromGmsFactory, MoleculeFromGmsFactory>();


                    break;
                case ServiceLifetime.Scoped:
                    services.AddScoped<ICalcOrderService, CalcOrderService>();
                    services.AddScoped<ICalcOrderItemService, CalcOrderItemService>();
                    services.AddScoped<ICalcMoleculeService, CalcMoleculeService>();
                    services.AddScoped<ICalcDeliveryService, CalcDeliveryService>();
                    services.AddScoped<IMoleculeReportService, MoleculeReportService>();
                    services.AddScoped<IMoleculeAnalysisService, MoleculeAnalysisService>();

                    services.AddScoped<IGmsCalcInputFactory, GmsCalcInputFactory>();
                    services.AddScoped<IMoleculeFromGmsFactory, MoleculeFromGmsFactory>();


                    break;
                case ServiceLifetime.Transient:
                    services.AddTransient<ICalcOrderService, CalcOrderService>();
                    services.AddTransient<ICalcOrderItemService, CalcOrderItemService>();
                    services.AddTransient<ICalcMoleculeService, CalcMoleculeService>();
                    services.AddTransient<ICalcDeliveryService, CalcDeliveryService>();
                    services.AddTransient<IMoleculeReportService, MoleculeReportService>();
                    services.AddTransient<IMoleculeAnalysisService, MoleculeAnalysisService>();

                    services.AddTransient<IGmsCalcInputFactory, GmsCalcInputFactory>();
                    services.AddTransient<IMoleculeFromGmsFactory, MoleculeFromGmsFactory>();
                    break;
            }

            return services;
        }
    }
}
