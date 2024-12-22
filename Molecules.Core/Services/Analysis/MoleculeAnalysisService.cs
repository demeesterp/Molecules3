using Molecules.Core.Domain.Entities;
using Molecules.Core.Domain.ValueObjects.Analysis.Population;
using Molecules.Core.Domain.ValueObjects.AtomData;
using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Population;
using Molecules.Core.Domain.ValueObjects.Molecules;
using Molecules.Core.Factories.Analysis;
using Molecules.Core.Services.CalcMolecules;
using Molecules.Shared.Logger;
using System.Linq;

namespace Molecules.Core.Services.Analysis
{
    public class MoleculeAnalysisService(ICalcMoleculeService calcMoleculeService, 
                                            IMoleculesVectorFactory moleculesVectorFactory,
                                                IMoleculesLogger logger) : IMoleculeAnalysisService
    {
        #region dependencies

        private readonly ICalcMoleculeService _calcMoleculeService = calcMoleculeService;

        private readonly IMoleculesVectorFactory _moleculesVectorFactor = moleculesVectorFactory;

        private readonly IMoleculesLogger _logger = logger;

        #endregion

        public async Task<MoleculeAtomPopulationAnalysisResult> DoAtomPopulationAnalysisAsync(int numberOfClusters)
        {
            MoleculeAtomPopulationAnalysisResult result = new MoleculeAtomPopulationAnalysisResult();
            List<MoleculeAtomPopulationVector> allVectors = new List<MoleculeAtomPopulationVector>();
            foreach (var molecule in (await _calcMoleculeService.GetAllByNameAsync("%")).Where(m => m.Molecule is not null))
            {
                allVectors.AddRange(from Atom atom in molecule.Molecule!.Atoms
                                    select _moleculesVectorFactor.CreateMoleculeAtomPopulationVector(atom, molecule.MoleculeName));
            }

            List<MoleculeAtomPopulationVectorCollection> subCollections = new List<MoleculeAtomPopulationVectorCollection>();
            foreach(var collection in allVectors.GroupBy(x => x.AtomNumber))
            {
                MoleculeAtomPopulationVectorCollection newCollections =
                    new MoleculeAtomPopulationVectorCollection(collection.Key);
                newCollections.AddVectors(collection.ToList());
                subCollections.Add(newCollections);
            }

            foreach(var atomCollection in subCollections)
            {
                var clusters = atomCollection.KMeansCluster(numberOfClusters);
                foreach (var cluster in clusters)
                {
                    var atomKind = AtomPropertiesTable.GetAtomProperties(atomCollection.AtomNumber);
                    if (atomKind is not null)
                    {
                        result.Results.Add(
                            new MoleculeAtomPopulationCategory(atomKind.Symbol,
                                                                cluster.Label,
                                                                    cluster.Vectors.Cast<MoleculeAtomPopulationVector>()
                            .ToList()));
                    }
                };
            }
            return result;
        }

    }
}
