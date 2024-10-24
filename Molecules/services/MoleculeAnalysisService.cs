using Molecules.Core.Domain.ValueObjects.Analysis;
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


            KMeans.Test();


            Console.ReadLine();


            var result = await _moleculeAnalysisService.CreateDataSetAsync(AnalysisTypeEnum.AtomChargeRules);
            string fileName = WriteResult(result, AnalysisTypeEnum.AtomChargeRules);
            Console.WriteLine($"Output written to {fileName}");
        }

        private string WriteResult(string fileContent, AnalysisTypeEnum analysisType)
        {
            string fileName = Path.Combine(_settings.AnalysisOutputPath, $"{analysisType}.txt");
            File.WriteAllText(fileName, fileContent);
            return fileName;
        }

    }
}
