using Molecules.Core.Domain.ValueObjects.Analysis.Population;
using Molecules.Core.Services.Analysis;
using Molecules.settings;

namespace Molecules.services
{
    public class MolAnalysisExecutionService
    {

        #region dependencies

        private readonly IMoleculesSettings _settings;

        private readonly IMoleculeAnalysisService _moleculeAnalysisService;

        #endregion


        public MolAnalysisExecutionService(IMoleculeAnalysisService moleculeAnalysisService,
                                                IMoleculesSettings settings)
        {
            _moleculeAnalysisService = moleculeAnalysisService;
            _settings = settings;
        }

        public async Task RunAsync()
        {
            Console.WriteLine($"Base directory is {_settings.BasePath}");
            Console.WriteLine($"Analysis output directory is {_settings.AnalysisOutputPath}");
            Console.Write("Number of centers : ");
            var numberOfCentersAnswer = Console.ReadLine();
            if (int.TryParse(numberOfCentersAnswer, out int numberOfCenters))
            {
                MoleculeAtomPopulationAnalysisResult result = await _moleculeAnalysisService.DoAtomPopulationAnalysisAsync(numberOfCenters);
                var resultsFile = Path.Combine(_settings.AnalysisOutputPath, "result.csv");
                File.WriteAllText(resultsFile, result.GetReport());
                Console.WriteLine($"Output written to {resultsFile}");
            }
        }
    }
}
