using Molecules.Core.Domain.ValueObjects.KMeansAnalysis;
using Molecules.Core.Services.Analysis;
using Molecules.settings;
using System.Text.Json;
using System.Text.Json.Serialization;

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
            Console.Write("Cluster input : ");
            var clusterInputCollection = GetUserInput();
            foreach(var input in clusterInputCollection.ClusterList.Where(x => x.Active))
            {
                var result = await _moleculeAnalysisService.DoAtomAnalysisAsync(input);
                var resultsFile = Path.Combine(_settings.AnalysisOutputPath, $"result{input.Type}.csv");
                File.WriteAllText(resultsFile, result.GetReport());
                Console.WriteLine($"Output written to {resultsFile}");
            }
        }

        private MoleculeAtomClusteringInputCollection GetUserInput()
        {
            MoleculeAtomClusteringInputCollection retval = ReadInputFromFile();
            foreach(var input in retval.ClusterList)
            {
                Console.WriteLine($"Define centers for type {input.Type}");
                Console.Write("Continue y/n :");
                var answer = Console.ReadLine();
                if (answer?.ToLower().Trim() != "y")
                {
                    input.Active = false;
                    continue;
                }

                input.Active = true;
                Console.WriteLine("Get number of centers per atom");
                foreach (var item in input.Items)
                {
                    Console.Write($"{item.Atom}: ({item.NbrOfClusters}) ");
                    var userResponse = Console.ReadLine();
                    if (int.TryParse(userResponse, out int currentNbrOfCenters))
                    {
                        item.NbrOfClusters = currentNbrOfCenters;
                    }
                }
            }
            WriteInputToFileAsync(retval);
            return retval;
        }

        private void WriteInputToFileAsync(MoleculeAtomClusteringInputCollection input)
        {
            var inputFilePath = Path.Combine(_settings.AnalysisOutputPath, "input.json");
            var options = new JsonSerializerOptions 
            { 
                WriteIndented = true,
                Converters = { new JsonStringEnumConverter() }
            };
            var json = JsonSerializer.Serialize(input, options);
            File.WriteAllText(inputFilePath, json);
        }

        private MoleculeAtomClusteringInputCollection ReadInputFromFile()
        {
            var inputFilePath = Path.Combine(_settings.AnalysisOutputPath, "input.json");
            if ( File.Exists(inputFilePath))
            {
                var content = File.ReadAllText(inputFilePath);

                return JsonSerializer.Deserialize<MoleculeAtomClusteringInputCollection>(content,
                                                new JsonSerializerOptions 
                                                { 
                                                    WriteIndented = true,
                                                    Converters = { new JsonStringEnumConverter() }
                                                })!;
            }
            else
            {
                return MoleculeAtomClusteringInputCollection.Default;
            }
        }
    }
}
