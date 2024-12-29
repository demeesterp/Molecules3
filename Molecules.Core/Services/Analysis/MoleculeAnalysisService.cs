using Molecules.Core.Domain.ValueObjects.AtomData;
using Molecules.Core.Domain.ValueObjects.KMeansAnalysis;
using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Base;
using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Base.Result;
using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Orbital.Result;
using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Orbital.Vectors;
using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Population.Result;
using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Population.Vectors;
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

        public async Task<IMoleculeAnalysisResult> DoAtomAnalysisAsync(MoleculeAtomClusteringInput clusters)
        {
            return clusters.Type switch
            {
                KMeansVectorTypeEnum.AtomPopulation => await DoAtomPopulationAnalysisAsync(clusters),
                KMeansVectorTypeEnum.AtomPopulationHomo => await DoAtomHomoPopulationAnalysisAsync(clusters),
                KMeansVectorTypeEnum.AtomPopulationLumo => await DoAtomLumoPopulationAnalysisAsync(clusters),
                KMeansVectorTypeEnum.AtomOrbitalPopulation => await DoAtomOrbitalPopulationAnalysisAsync(clusters),
                KMeansVectorTypeEnum.AtomOrbitalPopulationLumo => throw new NotSupportedException($"The cluster type {clusters.Type} is not supported."),
                KMeansVectorTypeEnum.AtomOrbitalPopulationHomo => throw new NotSupportedException($"The cluster type {clusters.Type} is not supported."), 
                _ => throw new NotSupportedException($"The cluster type {clusters.Type} is not supported.")
            };
        }

        private async Task<MoleculeAtomPopulationAnalysisResult> DoAtomPopulationAnalysisAsync(MoleculeAtomClusteringInput clusters)
        {
            MoleculeAtomPopulationAnalysisResult result = new MoleculeAtomPopulationAnalysisResult();
            if (clusters.Type == KMeansVectorTypeEnum.AtomPopulation)
            {
                foreach (var vectorsToCluster in _moleculesVectorCollectionFactory.CreateMoleculeAtomPopulationVectorCollection(await _calcMoleculeService.GetAllByNameAsync("%")))
                {
                    var atomKind = AtomPropertiesTable.GetAtomProperties(vectorsToCluster.AtomNumber);
                    if (atomKind != null)
                    {
                        var numberOfClusters = clusters.Items.Find(x => x.Atom == atomKind.Symbol);
                        var categories = _moleculeClusterService.KMeansCluster<MoleculeAtomPopulationVector>(vectorsToCluster, numberOfClusters?.NbrOfClusters ?? 3);
                        foreach (MoleculesCluster<MoleculeAtomPopulationVector> category in categories)
                        {
                            result.Categories.Add(new MoleculeAtomCategory<MoleculeAtomPopulationVector>(atomKind.Symbol, category));
                        }
                    }
                }
            }
            return result;
        }

        private async Task<MoleculeAtomPopulationLumoAnalysisResult> DoAtomLumoPopulationAnalysisAsync(MoleculeAtomClusteringInput clusters)
        {
            MoleculeAtomPopulationLumoAnalysisResult result = new MoleculeAtomPopulationLumoAnalysisResult();
            if (clusters.Type == KMeansVectorTypeEnum.AtomPopulationLumo)
            {
                foreach (var vectorsToCluster in _moleculesVectorCollectionFactory.CreateMoleculeAtomPopulationLumoVectorCollection(await _calcMoleculeService.GetAllByNameAsync("%")))
                {
                    var atomKind = AtomPropertiesTable.GetAtomProperties(vectorsToCluster.AtomNumber);
                    if (atomKind != null)
                    {
                        var numberOfClusters = clusters.Items.Find(x => x.Atom == atomKind.Symbol);
                        var categories = _moleculeClusterService.KMeansCluster<MoleculeAtomPopulationLumoVector>(vectorsToCluster, numberOfClusters?.NbrOfClusters ?? 3);
                        foreach (MoleculesCluster<MoleculeAtomPopulationLumoVector> category in categories)
                        {
                            result.Categories.Add(new MoleculeAtomCategory<MoleculeAtomPopulationLumoVector>(atomKind.Symbol, category));
                        }
                    }
                }
            }
            return result;
        }

        private async Task<MoleculeAtomPopulationHomoAnalysisResult> DoAtomHomoPopulationAnalysisAsync(MoleculeAtomClusteringInput clusters)
        {
            MoleculeAtomPopulationHomoAnalysisResult result = new MoleculeAtomPopulationHomoAnalysisResult();
            if (clusters.Type == KMeansVectorTypeEnum.AtomOrbitalPopulationHomo)
            {
                foreach (var vectorsToCluster in _moleculesVectorCollectionFactory.CreateMoleculeAtomPopulationHomoVectorCollection(await _calcMoleculeService.GetAllByNameAsync("%")))
                {
                    var atomKind = AtomPropertiesTable.GetAtomProperties(vectorsToCluster.AtomNumber);
                    if (atomKind != null)
                    {
                        var numberOfClusters = clusters.Items.Find(x => x.Atom == atomKind.Symbol);
                        var categories = _moleculeClusterService.KMeansCluster<MoleculeAtomPopulationHomoVector>(vectorsToCluster, numberOfClusters?.NbrOfClusters ?? 3);
                        foreach (MoleculesCluster<MoleculeAtomPopulationHomoVector> category in categories)
                        {
                            result.Categories.Add(new MoleculeAtomCategory<MoleculeAtomPopulationHomoVector>(atomKind.Symbol, category));
                        }
                    }
                }
            }
            return result;
        }


        private async Task<MoleculeAtomOrbitalPopulationAnalysisResult> DoAtomOrbitalPopulationAnalysisAsync(MoleculeAtomClusteringInput clusters)
        {
            MoleculeAtomOrbitalPopulationAnalysisResult result = new MoleculeAtomOrbitalPopulationAnalysisResult();

            if (clusters.Type == KMeansVectorTypeEnum.AtomOrbitalPopulation)
            {
                foreach (var vectorsToCluster in _moleculesVectorCollectionFactory.CreateMoleculeAtomOrbitalPopulationVectorCollection(await _calcMoleculeService.GetAllByNameAsync("%")))
                {
                    var atomKind = AtomPropertiesTable.GetAtomProperties(vectorsToCluster.AtomNumber);
                    if (atomKind != null)
                    {
                        var numberOfClusters = clusters.Items.Find(x => x.Atom == atomKind.Symbol);
                        var categories = _moleculeClusterService.KMeansCluster<MoleculeAtomOrbitalPopulationVector>(vectorsToCluster, numberOfClusters?.NbrOfClusters ?? 3);
                        foreach (MoleculesCluster<MoleculeAtomOrbitalPopulationVector> category in categories)
                        {
                            result.Categories.Add(new MoleculeAtomCategory<MoleculeAtomOrbitalPopulationVector>(atomKind.Symbol, category));
                        }
                    }
                }
            }

            return result;
        }

    }
}
