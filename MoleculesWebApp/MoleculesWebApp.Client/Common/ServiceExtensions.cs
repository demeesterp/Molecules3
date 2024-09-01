using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MoleculesWebApp.Client.Services.Molecules;
using MoleculesWebApp.Client.Shared.Error;
using MoleculesWebApp.Client.Shared.HttpClientHelper;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using System.Net;
using MoleculesWebApp.Client.Services;
using MoleculesWebApp.Client.Services.OrderBook.ServiceAgent;
using MoleculesWebApp.Client.Factory;
using MoleculesWebApp.Client.Services.OrderBook;
using MoleculesWebApp.Client.Services.Calculation;
using MoleculesWebApp.Client.Services.Molecules.ServiceAgent;

namespace MoleculesWebApp.Client.Common
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterClientServices(this IServiceCollection services,
                                                                    IWebAssemblyHostEnvironment environment)
        {
            return services.RegisterServices().RegisterFactory().RegisterHttpClient(environment);
        }

        private static IServiceCollection RegisterHttpClient(this IServiceCollection services,
                                                                    IWebAssemblyHostEnvironment environment)
        {
            // Molecule orders service agent
            services.AddHttpClient<ICalcOrderServiceAgent, CalcOrderServiceAgent>(client =>
            {
                client.BaseAddress = new Uri(environment.GetApiBasePath());
            })
            .AddPolicyHandler(GetRetryPolicy());

            // Molecule service agents
            services.AddHttpClient<IMoleculesServiceAgent, MoleculesServiceAgent>(client =>
            {
                client.BaseAddress = new Uri(environment.GetApiBasePath());
            })
           .AddPolicyHandler(GetRetryPolicy());

            // Molecule report service agents
            services.AddHttpClient<IMoleculesReportServiceAgent, MoleculesReportServiceAgent>(client =>
            {
                client.BaseAddress = new Uri(environment.GetApiBasePath());
            })
           .AddPolicyHandler(GetRetryPolicy());

            return services;
        }


        private static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<MoleculesHttpClient>();
            services.AddSingleton<ErrorHandlingService>();

            services.AddSingleton<ICalcOrderServiceAgent, CalcOrderServiceAgent>();
            services.AddSingleton<ICalcOrderService, CalcOrderService>();

            services.AddSingleton<IBasisSetService, BasisSetService>();
            services.AddSingleton<ICalcOrderItemTypeService, CalcOrderItemTypeService>();
            
            services.AddSingleton<IMoleculesServiceAgent, MoleculesServiceAgent>();
            services.AddSingleton<IMoleculesAnalysisService, MoleculesAnalysisService>();
            services.AddSingleton<IMoleculesReportServiceAgent, MoleculesReportServiceAgent>();
            
            return services;
        }


        private static IServiceCollection RegisterFactory(this IServiceCollection services)
        {
            services.AddSingleton<ICalcOrderFactory, CalcOrderFactory>();
            services.AddSingleton<ICalcOrderItemFactory, CalcOrderItemFactory>();
            return services;
        }


        private static AsyncRetryPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }

    }
}
