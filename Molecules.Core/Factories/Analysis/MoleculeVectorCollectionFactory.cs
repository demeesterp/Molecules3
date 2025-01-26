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

        public List<MoleculeAtomHomoPopulationVectorCollection> CreateMoleculeAtomPopulationHomoVectorCollection(List<CalcMolecule> molecules)
        {
            List<MoleculeAtomHomoPopulationVectorCollection> retval = new List<MoleculeAtomHomoPopulationVectorCollection>();
            List<MoleculeAtomHomoPopulationVector> allVectors = new List<MoleculeAtomHomoPopulationVector>();

            foreach (var molecule in molecules.Where(m => m.Molecule is not null))
            {
                allVectors.AddRange(from Atom atom in molecule.Molecule!.Atoms
                                    select _moleculesVectorFactory.CreateMoleculeAtomPopulationHomoVector(atom, molecule.Molecule));
            }

            foreach (var collection in allVectors.GroupBy(x => x.Values.AtomNumber))
            {
                MoleculeAtomHomoPopulationVectorCollection newCollections = new(collection.Key);
                newCollections.AddVectors(collection.ToList());
                retval.Add(newCollections);
            }

            foreach (var item in retval)
            {
                item.Normalize();
            }

            return retval;
        }

        public List<MoleculeAtomLumoPopulationVectorCollection> CreateMoleculeAtomPopulationLumoVectorCollection(List<CalcMolecule> molecules)
        {
            List<MoleculeAtomLumoPopulationVectorCollection> retval = new List<MoleculeAtomLumoPopulationVectorCollection>();
            List<MoleculeAtomLumoPopulationVector> allVectors = new List<MoleculeAtomLumoPopulationVector>();

            foreach (var molecule in molecules.Where(m => m.Molecule is not null))
            {
                allVectors.AddRange(from Atom atom in molecule.Molecule!.Atoms
                                    select _moleculesVectorFactory.CreateMoleculeAtomPopulationLumoVector(atom, molecule.Molecule));
            }

            foreach (var collection in allVectors.GroupBy(x => x.Values.AtomNumber))
            {
                MoleculeAtomLumoPopulationVectorCollection newCollections = new(collection.Key);
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

        public List<MoleculeAtomOrbitalHomoPopulationVectorCollection> CreateMoleculeAtomOrbitalHomoPopulationVectorCollection(List<CalcMolecule> molecules)
        {
            List<MoleculeAtomOrbitalHomoPopulationVectorCollection> retval = new List<MoleculeAtomOrbitalHomoPopulationVectorCollection>();
            List<MoleculeAtomOrbitalHomoPopulationVector> allVectors = new List<MoleculeAtomOrbitalHomoPopulationVector>();

            foreach (var molecule in molecules.Where(m => m.Molecule is not null))
            {
                allVectors.AddRange(from Atom atom in molecule.Molecule!.Atoms
                                    select _moleculesVectorFactory.CreateMoleculesAtomOrbitalHomoPopulationVector(atom, molecule.Molecule));
            }

            foreach (var collection in allVectors.Where(x => x.Dimensions > 0).GroupBy(x => x.Values.AtomNumber))
            {
                MoleculeAtomOrbitalHomoPopulationVectorCollection newCollections = new(collection.Key);
                newCollections.AddVectors(collection.ToList());
                retval.Add(newCollections);
            }

            foreach (var item in retval)
            {
                item.Normalize();
            }

            return retval;
        }

        public List<MoleculeAtomOrbitalLumoPopulationVectorCollection> CreateMoleculeAtomOrbitalLumoPopulationVectorCollection(List<CalcMolecule> molecules)
        {
            List<MoleculeAtomOrbitalLumoPopulationVectorCollection> retval = new List<MoleculeAtomOrbitalLumoPopulationVectorCollection>();
            List<MoleculeAtomOrbitalLumoPopulationVector> allVectors = new List<MoleculeAtomOrbitalLumoPopulationVector>();

            foreach (var molecule in molecules.Where(m => m.Molecule is not null))
            {
                allVectors.AddRange(from Atom atom in molecule.Molecule!.Atoms
                                    select _moleculesVectorFactory.CreateMoleculesAtomOrbitalLumoPopulationVector(atom, molecule.Molecule));
            }

            foreach (var collection in allVectors.Where(x => x.Dimensions > 0).GroupBy(x => x.Values.AtomNumber))
            {
                MoleculeAtomOrbitalLumoPopulationVectorCollection newCollections = new(collection.Key);
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
