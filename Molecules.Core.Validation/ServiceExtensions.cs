using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Molecules.Core.Services.CalcOrders.Validation;
using Molecules.Core.Validation.Services;
using System.Reflection;

namespace Molecules.Core.Validation
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddValidationServices(this IServiceCollection services, ServiceLifetime serviceLifetime)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), serviceLifetime);
            switch (serviceLifetime)
            {
                case ServiceLifetime.Singleton:
                    services.AddSingleton<ICalcOrderServiceValidations, CalcOrderServiceValidations>();

                    services.AddSingleton<ICalcOrderItemServiceValidations, CalcOrderItemServiceValidations>();
                    
                    break;
                case ServiceLifetime.Scoped:
                    services.AddScoped<ICalcOrderServiceValidations, CalcOrderServiceValidations>();

                    services.AddScoped<ICalcOrderItemServiceValidations, CalcOrderItemServiceValidations>();

                    break;
                case ServiceLifetime.Transient:
                    services.AddTransient<ICalcOrderServiceValidations, CalcOrderServiceValidations>();

                    services.AddTransient<ICalcOrderItemServiceValidations, CalcOrderItemServiceValidations>();

                    break;
            }

            return services;
        }
    }
}
