using Molecules.Core.Domain.ValueObjects.Analysis.Population;
using Molecules.Core.Domain.ValueObjects.KMeansAnalysis;
using Molecules.Core.Services.Analysis;
using Molecules.settings;
using System.Text.Json;

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
            var result = await _moleculeAnalysisService.DoAtomPopulationAnalysisAsync(GetUserInput());
            var resultsFile = Path.Combine(_settings.AnalysisOutputPath, "result.csv");
            File.WriteAllText(resultsFile, result.GetReport());
            Console.WriteLine($"Output written to {resultsFile}");
        }

        private MoleculeAtomClusteringInput GetUserInput()
        {
            MoleculeAtomClusteringInput retval = ReadInputFromFile();
            Console.WriteLine("Get number of centers per atom");
            foreach(var item in retval.Items)
            {
                Console.Write($"{item.Atom}: ({item.NbrOfClusters}) ");
                var userResponse = Console.ReadLine();
                if (int.TryParse(userResponse, out int currentNbrOfCenters))
                {
                    item.NbrOfClusters = currentNbrOfCenters;
                }
            }
            WriteInputToFileAsync(retval);
            return retval;
        }

        private void WriteInputToFileAsync(MoleculeAtomClusteringInput input)
        {
            var inputFilePath = Path.Combine(_settings.AnalysisOutputPath, "input.json");
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(input, options);
            File.WriteAllText(inputFilePath, json);
        }


        private MoleculeAtomClusteringInput ReadInputFromFile()
        {
            var inputFilePath = Path.Combine(_settings.AnalysisOutputPath, "input.json");
            if ( File.Exists(inputFilePath))
            {
                var content = File.ReadAllText(inputFilePath);

                return JsonSerializer.Deserialize<MoleculeAtomClusteringInput>(content,
                                                new JsonSerializerOptions { WriteIndented = true })!;
            }
            else
            {
                return new MoleculeAtomClusteringInput();
            }
        }



    }
}
