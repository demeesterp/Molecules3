using Molecules.Core.Domain.Entities;
using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Population;
using Molecules.Core.Domain.ValueObjects.Molecules;

namespace Molecules.Core.Factories.Analysis
{
    public class MoleculeVectorCollectionFactory : IMoleculeVectorCollectionFactory
    {

        private IMoleculesVectorFactory _moleculesVectorFactory;

        public MoleculeVectorCollectionFactory(IMoleculesVectorFactory moleculesVectorFactory)
        {
            _moleculesVectorFactory = moleculesVectorFactory;
        }


        public List<MoleculeAtomPopulationVectorCollection> CreateMoleculeAtomPopulationVectorCollection(List<CalcMolecule> molecules)
        {
            List<MoleculeAtomPopulationVectorCollection> retval = new List<MoleculeAtomPopulationVectorCollection>();
            List<MoleculeAtomPopulationVector> allVectors = new List<MoleculeAtomPopulationVector>();
            
            foreach (var molecule in molecules.Where(m => m.Molecule is not null))
            {
                allVectors.AddRange(from Atom atom in
                                        molecule.Molecule!.Atoms
                                    select 
                                        _moleculesVectorFactory.CreateMoleculeAtomPopulationVector(atom, molecule.MoleculeName));
            }

            foreach (var collection in allVectors.GroupBy(x => x.AtomNumber))
            {
                MoleculeAtomPopulationVectorCollection newCollections =
                    new MoleculeAtomPopulationVectorCollection(collection.Key);
                newCollections.AddVectors(collection.ToList());
                retval.Add(newCollections);
            }

            return retval;
        }
    }
}
