using Molecules.Core.Domain.Entities;
using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Orbital.VectorCollections;
using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Orbital.Vectors;
using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Population.VectorCollection;
using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Population.Vectors;
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

        public List<MoleculeAtomOrbitalPopulationVectorCollection> CreateMoleculeAtomOrbitalPopulationVectorCollection(List<CalcMolecule> molecules)
        {
            List<MoleculeAtomOrbitalPopulationVectorCollection> retval = new List<MoleculeAtomOrbitalPopulationVectorCollection>();
            List<MoleculeAtomOrbitalPopulationVector> allVectors = new List<MoleculeAtomOrbitalPopulationVector>();

            foreach (var molecule in molecules.Where(m => m.Molecule is not null))
            {
                allVectors.AddRange(from Atom atom in molecule.Molecule!.Atoms
                                                select _moleculesVectorFactory.CreateMoleculesAtomOrbitalPopulationVector(atom, molecule.Molecule));
            }

            foreach (var collection in allVectors.Where(x => x.Dimensions > 0).GroupBy(x => x.Values.AtomNumber))
            {
                MoleculeAtomOrbitalPopulationVectorCollection newCollections = new(collection.Key);
                newCollections.AddVectors(collection.ToList());
                retval.Add(newCollections);
            }

            foreach (var item in retval)
            {
                item.Normalize();
            }

            return retval;
        }

        public List<MoleculeAtomPopulationHomoVectorCollection> CreateMoleculeAtomPopulationHomoVectorCollection(List<CalcMolecule> molecules)
        {
            List<MoleculeAtomPopulationHomoVectorCollection> retval = new List<MoleculeAtomPopulationHomoVectorCollection>();
            List<MoleculeAtomPopulationHomoVector> allVectors = new List<MoleculeAtomPopulationHomoVector>();

            foreach (var molecule in molecules.Where(m => m.Molecule is not null))
            {
                allVectors.AddRange(from Atom atom in molecule.Molecule!.Atoms
                                    select _moleculesVectorFactory.CreateMoleculeAtomPopulationHomoVector(atom, molecule.Molecule));
            }

            foreach (var collection in allVectors.GroupBy(x => x.Values.AtomNumber))
            {
                MoleculeAtomPopulationHomoVectorCollection newCollections = new(collection.Key);
                newCollections.AddVectors(collection.ToList());
                retval.Add(newCollections);
            }

            foreach (var item in retval)
            {
                item.Normalize();
            }

            return retval;
        }

        public List<MoleculeAtomPopulationLumoVectorCollection> CreateMoleculeAtomPopulationLumoVectorCollection(List<CalcMolecule> molecules)
        {
            List<MoleculeAtomPopulationLumoVectorCollection> retval = new List<MoleculeAtomPopulationLumoVectorCollection>();
            List<MoleculeAtomPopulationLumoVector> allVectors = new List<MoleculeAtomPopulationLumoVector>();

            foreach (var molecule in molecules.Where(m => m.Molecule is not null))
            {
                allVectors.AddRange(from Atom atom in molecule.Molecule!.Atoms
                                    select _moleculesVectorFactory.CreateMoleculeAtomPopulationLumoVector(atom, molecule.Molecule));
            }

            foreach (var collection in allVectors.GroupBy(x => x.Values.AtomNumber))
            {
                MoleculeAtomPopulationLumoVectorCollection newCollections = new(collection.Key);
                newCollections.AddVectors(collection.ToList());
                retval.Add(newCollections);
            }

            foreach (var item in retval)
            {
                item.Normalize();
            }

            return retval;
        }

        public List<MoleculeAtomPopulationVectorCollection> CreateMoleculeAtomPopulationVectorCollection(List<CalcMolecule> molecules)
        {
            List<MoleculeAtomPopulationVectorCollection> retval = new List<MoleculeAtomPopulationVectorCollection>();
            List<MoleculeAtomPopulationVector> allVectors = new List<MoleculeAtomPopulationVector>();
            
            foreach (var molecule in molecules.Where(m => m.Molecule is not null))
            {
               allVectors.AddRange(from Atom atom in molecule.Molecule!.Atoms
                                    select _moleculesVectorFactory.CreateMoleculeAtomPopulationVector(atom, molecule.Molecule));
            }

            foreach (var collection in allVectors.GroupBy(x => x.Values.AtomNumber))
            {
                MoleculeAtomPopulationVectorCollection newCollections = new (collection.Key);
                newCollections.AddVectors(collection.ToList());
                retval.Add(newCollections);
            }

            foreach (var item in retval)
            {
                item.Normalize();
            }

            return retval;
        }
    }
}
