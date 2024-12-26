using Molecules.Core.Domain.ValueObjects.Analysis.Population;
using Molecules.Core.Domain.ValueObjects.AtomData;
using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Base;
using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Population;
using Molecules.Core.Factories.Analysis;
using Molecules.Core.Services.Analysis.Clustering;
using Molecules.Core.Services.CalcMolecules;
using Molecules.Shared.Logger;
using System.Text;

namespace Molecules.Core.Services.Analysis
{
    public class MoleculeAnalysisService(ICalcMoleculeService calcMoleculeService,
                                            IMoleculeVectorCollectionFactory moleculesVectorCollectionFactory,
                                            IMoleculeClusterService moleculeClusterService,
                                                IMoleculesLogger logger) : IMoleculeAnalysisService
    {
        #region dependencies

        private readonly ICalcMoleculeService _calcMoleculeService = calcMoleculeService;

        private readonly IMoleculeVectorCollectionFactory _moleculesVectorCollectionFactory = moleculesVectorCollectionFactory;

        private readonly IMoleculeClusterService _moleculeClusterService = moleculeClusterService;

        private readonly IMoleculesLogger _logger = logger;

        #endregion

        public async Task<MoleculeAtomPopulationAnalysisResult> DoAtomPopulationAnalysisAsync(int numberOfClusters)
        {
            MoleculeAtomPopulationAnalysisResult result = new MoleculeAtomPopulationAnalysisResult();
            foreach (var vectorsToCluster in _moleculesVectorCollectionFactory.CreateMoleculeAtomPopulationVectorCollection(await _calcMoleculeService.GetAllByNameAsync("%")))
            {

                var categories = _moleculeClusterService.KMeansCluster<MoleculeAtomPopulationVector>(vectorsToCluster, numberOfClusters);
                foreach (MoleculesCluster<MoleculeAtomPopulationVector> category in categories)
                {
                    var atomKind = AtomPropertiesTable.GetAtomProperties(vectorsToCluster.AtomNumber);
                    if (atomKind != null)
                    {
                        result.Categories.Add(new MoleculeAtomPopulationCategory(atomKind.Symbol, category));
                    }
                }
            }
            return result;
        }

        public async Task<string> EvaluateNumberOfClusters()
        {
            StringBuilder result = new StringBuilder();
            foreach (var vectorsToCluster in 
                            _moleculesVectorCollectionFactory.
                                    CreateMoleculeAtomPopulationVectorCollection(await _calcMoleculeService.GetAllByNameAsync("%")))
            {

                if (vectorsToCluster.Count > 2)
                {
                    var displayResult = _moleculeClusterService.CalculateWCSSForRange<MoleculeAtomPopulationVector>(vectorsToCluster,
                                                                                                                    2,
                                                                                                                    Math.Min(vectorsToCluster.Count, 10));
                    foreach (var item in displayResult)
                    {
                        result.AppendLine($"AtomNumber {vectorsToCluster.AtomNumber} : Value {item.Item1} Cluster {item.Item2}");
                    }
                }
                else
                {
                    result.AppendLine($"AtomNumber {vectorsToCluster.AtomNumber} : 2 clusters max");
                }

                
            }
            return result.ToString();
        }


    }
}
