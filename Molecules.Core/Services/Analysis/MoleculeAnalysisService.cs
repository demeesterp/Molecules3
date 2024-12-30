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
                KMeansVectorTypeEnum.AtomOrbitalPopulationLumo => await DoAtomOrbitalLumoPopulationAnalysisAsync(clusters),
                KMeansVectorTypeEnum.AtomOrbitalPopulationHomo =>  await DoAtomOrbitalHomoPopulationAnalysisAsync(clusters), 
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

        private async Task<MoleculeAtomLumoPopulationAnalysisResult> DoAtomLumoPopulationAnalysisAsync(MoleculeAtomClusteringInput clusters)
        {
            MoleculeAtomLumoPopulationAnalysisResult result = new MoleculeAtomLumoPopulationAnalysisResult();
            if (clusters.Type == KMeansVectorTypeEnum.AtomPopulationLumo)
            {
                foreach (var vectorsToCluster in _moleculesVectorCollectionFactory.CreateMoleculeAtomPopulationLumoVectorCollection(await _calcMoleculeService.GetAllByNameAsync("%")))
                {
                    var atomKind = AtomPropertiesTable.GetAtomProperties(vectorsToCluster.AtomNumber);
                    if (atomKind != null)
                    {
                        var numberOfClusters = clusters.Items.Find(x => x.Atom == atomKind.Symbol);
                        var categories = _moleculeClusterService.KMeansCluster<MoleculeAtomLumoPopulationVector>(vectorsToCluster, numberOfClusters?.NbrOfClusters ?? 3);
                        foreach (MoleculesCluster<MoleculeAtomLumoPopulationVector> category in categories)
                        {
                            result.Categories.Add(new MoleculeAtomCategory<MoleculeAtomLumoPopulationVector>(atomKind.Symbol, category));
                        }
                    }
                }
            }
            return result;
        }

        private async Task<MoleculeAtomHomoPopulationAnalysisResult> DoAtomHomoPopulationAnalysisAsync(MoleculeAtomClusteringInput clusters)
        {
            MoleculeAtomHomoPopulationAnalysisResult result = new MoleculeAtomHomoPopulationAnalysisResult();
            if (clusters.Type == KMeansVectorTypeEnum.AtomOrbitalPopulationHomo)
            {
                foreach (var vectorsToCluster in _moleculesVectorCollectionFactory.CreateMoleculeAtomPopulationHomoVectorCollection(await _calcMoleculeService.GetAllByNameAsync("%")))
                {
                    var atomKind = AtomPropertiesTable.GetAtomProperties(vectorsToCluster.AtomNumber);
                    if (atomKind != null)
                    {
                        var numberOfClusters = clusters.Items.Find(x => x.Atom == atomKind.Symbol);
                        var categories = _moleculeClusterService.KMeansCluster<MoleculeAtomHomoPopulationVector>(vectorsToCluster, numberOfClusters?.NbrOfClusters ?? 3);
                        foreach (MoleculesCluster<MoleculeAtomHomoPopulationVector> category in categories)
                        {
                            result.Categories.Add(new MoleculeAtomCategory<MoleculeAtomHomoPopulationVector>(atomKind.Symbol, category));
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

        private async Task<MoleculeAtomOrbitalHomoPopulationAnalysisResult> DoAtomOrbitalHomoPopulationAnalysisAsync(MoleculeAtomClusteringInput clusters)
        {
            MoleculeAtomOrbitalHomoPopulationAnalysisResult result = new MoleculeAtomOrbitalHomoPopulationAnalysisResult();

            if (clusters.Type == KMeansVectorTypeEnum.AtomOrbitalPopulationHomo)
            {
                foreach (var vectorsToCluster in _moleculesVectorCollectionFactory.CreateMoleculeAtomOrbitalHomoPopulationVectorCollection(await _calcMoleculeService.GetAllByNameAsync("%")))
                {
                    var atomKind = AtomPropertiesTable.GetAtomProperties(vectorsToCluster.AtomNumber);
                    if (atomKind != null)
                    {
                        var numberOfClusters = clusters.Items.Find(x => x.Atom == atomKind.Symbol);
                        var categories = _moleculeClusterService.KMeansCluster<MoleculeAtomOrbitalHomoPopulationVector>(vectorsToCluster, numberOfClusters?.NbrOfClusters ?? 3);
                        foreach (MoleculesCluster<MoleculeAtomOrbitalHomoPopulationVector> category in categories)
                        {
                            result.Categories.Add(new MoleculeAtomCategory<MoleculeAtomOrbitalHomoPopulationVector>(atomKind.Symbol, category));
                        }
                    }
                }
            }

            return result;
        }

        private async Task<MoleculeAtomOrbitalLumoPopulationAnalysisResult> DoAtomOrbitalLumoPopulationAnalysisAsync(MoleculeAtomClusteringInput clusters)
        {
            MoleculeAtomOrbitalLumoPopulationAnalysisResult result = new MoleculeAtomOrbitalLumoPopulationAnalysisResult();

            if (clusters.Type == KMeansVectorTypeEnum.AtomOrbitalPopulationLumo)
            {
                foreach (var vectorsToCluster in _moleculesVectorCollectionFactory.CreateMoleculeAtomOrbitalLumoPopulationVectorCollection(await _calcMoleculeService.GetAllByNameAsync("%")))
                {
                    var atomKind = AtomPropertiesTable.GetAtomProperties(vectorsToCluster.AtomNumber);
                    if (atomKind != null)
                    {
                        var numberOfClusters = clusters.Items.Find(x => x.Atom == atomKind.Symbol);
                        var categories = _moleculeClusterService.KMeansCluster<MoleculeAtomOrbitalLumoPopulationVector>(vectorsToCluster, numberOfClusters?.NbrOfClusters ?? 3);
                        foreach (MoleculesCluster<MoleculeAtomOrbitalLumoPopulationVector> category in categories)
                        {
                            result.Categories.Add(new MoleculeAtomCategory<MoleculeAtomOrbitalLumoPopulationVector>(atomKind.Symbol, category));
                        }
                    }
                }
            }

            return result;
        }

    }
}
