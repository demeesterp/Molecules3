using Molecules.Core.Domain.ValueObjects.Analysis.Population;
using Molecules.Core.Domain.ValueObjects.AtomData;
using Molecules.Core.Domain.ValueObjects.KMeansAnalysis;
using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Base;
using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Population;
using Molecules.Core.Factories.Analysis;
using Molecules.Core.Services.Analysis.Clustering;
using Molecules.Core.Services.CalcMolecules;
using Molecules.Shared.Logger;

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

        public async Task<MoleculeAtomPopulationAnalysisResult> DoAtomPopulationAnalysisAsync(MoleculeAtomClusteringInput clusters)
        {
            MoleculeAtomPopulationAnalysisResult result = new MoleculeAtomPopulationAnalysisResult();
            foreach (var vectorsToCluster in _moleculesVectorCollectionFactory.CreateMoleculeAtomPopulationVectorCollection(await _calcMoleculeService.GetAllByNameAsync("%")))
            {
                var atomKind = AtomPropertiesTable.GetAtomProperties(vectorsToCluster.AtomNumber);
                if ( atomKind != null)
                {
                    var numberOfClusters = clusters.Items.Find(x => x.Atom == atomKind.Symbol);
                    var categories = _moleculeClusterService.KMeansCluster<MoleculeAtomPopulationVector>(vectorsToCluster, numberOfClusters?.NbrOfClusters ?? 3);
                    foreach (MoleculesCluster<MoleculeAtomPopulationVector> category in categories)
                    {
                        result.Categories.Add(new MoleculeAtomPopulationCategory(atomKind.Symbol, category));
                    }
                }
            }
            return result;
        }

    }
}
