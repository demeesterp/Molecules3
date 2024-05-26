using Microsoft.Extensions.DependencyInjection;
using Molecules.Core.Services.CalcOrders;

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
                    break;
                case ServiceLifetime.Scoped:
                    services.AddScoped<ICalcOrderService, CalcOrderService>();
                    services.AddScoped<ICalcOrderItemService, CalcOrderItemService>();
                    break;
                case ServiceLifetime.Transient:
                    services.AddTransient<ICalcOrderService, CalcOrderService>();
                    services.AddTransient<ICalcOrderItemService, CalcOrderItemService>();
                    break;
            }

            return services;
        }
    }
}
