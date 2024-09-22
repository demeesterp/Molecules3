using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Molecules.constants;
using Molecules.services;
using Molecules.settings;
using Molecules.Shared.Logger;

namespace Molecules
{
    public class MoleculesApp : BackgroundService
    {
        #region dependencies

        private readonly IMoleculesLogger _logger;

        private readonly IMoleculesSettings _settings;

        private readonly IHostApplicationLifetime _hostApplicationLifetime;

        private readonly CalcDeliveryServices _calcDeliveryApp;

        private readonly MoleculeReportService _moleculeReportApp;

        private readonly MoleculeAnalysisService _moleculeAnalysisService;

        #endregion

        public MoleculesApp(CalcDeliveryServices calcDeliveryApp,
                                    MoleculeReportService moleculeReportApp,
                                        MoleculeAnalysisService moleculeAnalysisService,
                                            IMoleculesSettings settings,
                                                IMoleculesLogger logger,
                                                    IHostApplicationLifetime hostApplicationLifetime)
        {
            _logger = logger;
            _settings = settings;
            _calcDeliveryApp = calcDeliveryApp;
            _moleculeReportApp = moleculeReportApp;
            _hostApplicationLifetime = hostApplicationLifetime;
            _moleculeAnalysisService = moleculeAnalysisService;
        }

        private AppName GetApp()
        {
            if (!_settings.App.HasValue)
            {
                foreach (var enumItem in Enum.GetValues<AppName>())
                {
                    Console.WriteLine("For {0} press {1}", enumItem.ToString(), (int)enumItem);
                }
                Console.Write("Your choice:");
                var userInput = Console.ReadLine();
                bool invalidInput = !int.TryParse(userInput, out int option) || !Enum.IsDefined(typeof(AppName), option);
                while (invalidInput)
                {
                    Console.WriteLine($"\"{userInput}\" is an invalid choice !");
                    Console.WriteLine("Try again or press 0 to exit.");
                    Console.Write("Your choice:");
                    userInput = Console.ReadLine();
                    invalidInput = !int.TryParse(userInput, out option) || !Enum.IsDefined(typeof(AppName), option);
                }
                return (AppName)option;
            }
            return _settings.App.Value;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("MoleculesApp running at: {time}", DateTimeOffset.Now);
            try
            {
                bool done = false;
                while (!stoppingToken.IsCancellationRequested && !done)
                {
                    AppName app = GetApp();
                    switch (app)
                    {
                        case AppName.CalcDeliveryApp:
                            // Run Calculation Delivery : Generate molecules in DB
                            await _calcDeliveryApp.RunAsync(_settings.BasePath);
                            continue;
                        case AppName.MoleculeReportApp:
                            // Write reports to consumers
                            await _moleculeReportApp.RunAsync();
                            done = true;
                            return;
                        case AppName.MoleculeAnalysisApp:
                            await _moleculeAnalysisService.RunAsync();
                            continue;
                        case AppName.Exit:
                            done = true;
                            return;
                        default:
                            Console.WriteLine($"Unknow application: {app}");
                            done = true;
                            return;
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Something went wrong");
                Console.WriteLine("An error happend please request support or retry!");
                Console.WriteLine("Press any key!");
                Console.ReadLine();
            }
            finally
            {
                _hostApplicationLifetime.StopApplication();
            }
        }
    }
}
