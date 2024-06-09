using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Molecules.Logger.Logger;
using Molecules.Shared.Logger;
using Serilog.Events;
using Serilog;

namespace Molecules.Logger
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddLoggerServices(this IServiceCollection services,
                                                    ServiceLifetime serviceLifetime, IConfigurationSection loggerConfig)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                    .WriteTo.File(path: loggerConfig["LogFilePath"] ?? string.Empty,
                         outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                         rollingInterval: Enum.TryParse(loggerConfig["LogFileRollingInterval"], out RollingInterval interval) ? interval : RollingInterval.Day,
                         restrictedToMinimumLevel: Enum.TryParse(loggerConfig["LogFileMinimumLevel"], out LogEventLevel fileLogLevel) ? fileLogLevel : LogEventLevel.Information)
                    .WriteTo.Console(restrictedToMinimumLevel: Enum.TryParse(loggerConfig["LogConsoleMinimumLevel"], out LogEventLevel consoleLogLevel) ? consoleLogLevel : LogEventLevel.Information)
                    .CreateLogger();
            
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
