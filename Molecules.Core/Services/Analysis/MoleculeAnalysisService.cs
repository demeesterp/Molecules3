using Molecules.Core.Domain.Entities;
using Molecules.Core.Domain.ValueObjects.Analysis.Population;
using Molecules.Core.Domain.ValueObjects.AtomData;
using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Population;
using Molecules.Core.Domain.ValueObjects.Molecules;
using Molecules.Core.Factories.Analysis;
using Molecules.Core.Services.CalcMolecules;
using Molecules.Shared.Logger;

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

        private async Task<List<Molecule>> GetAllMoleculesAsync()
        {
            List<Molecule> moleculeList = new List<Molecule>();
            var molecules = await _calcMoleculeService.FindAllByNameAsync("%");
            foreach (CalcMolecule mol in molecules)
            {
                CalcMolecule fullResult = await _calcMoleculeService.GetAsync(mol.Id);
                if (fullResult.Molecule is not null)
                {
                    moleculeList.Add(fullResult.Molecule);
                }
            }
            return moleculeList;
        }

        public async Task<MoleculeAtomPopulationAnalysisResult> DoAtomPopulationAnalysisAsync(int numberOfClusters)
        {
            MoleculeAtomPopulationAnalysisResult result = new MoleculeAtomPopulationAnalysisResult();
            List<MoleculeAtomPopulationVector> allVectors = new List<MoleculeAtomPopulationVector>();
            foreach (Molecule molecule in await GetAllMoleculesAsync())
            {
                foreach(Atom atom in molecule.Atoms)
                {
                    allVectors.Add(_moleculesVectorFactor.CreateMoleculeAtomPopulationVector(atom, molecule.Name));
                }
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
