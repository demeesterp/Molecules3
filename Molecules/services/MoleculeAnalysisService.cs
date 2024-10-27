using Molecules.Core.Services.Analysis;
using Molecules.settings;

namespace Molecules.services
{
    public class MoleculeAnalysisService
    {

        #region dependencies

        private readonly IMoleculesSettings _settings;

        private readonly IMoleculeAnalysisService _moleculeAnalysisService;

        #endregion


        public MoleculeAnalysisService(IMoleculeAnalysisService moleculeAnalysisService,
                                                IMoleculesSettings settings)
        {
            _moleculeAnalysisService = moleculeAnalysisService;
            _settings = settings;
        }

        public async Task RunAsync()
        {
            Console.WriteLine($"Base directory is {_settings.BasePath}");
            Console.WriteLine($"Analysis output directory is {_settings.AnalysisOutputPath}");

            await Task.CompletedTask;
           
            Console.WriteLine($"Output written");
        }
    }
}
