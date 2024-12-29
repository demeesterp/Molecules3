using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Orbital.Vectors;
using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Population.Vectors;
using Molecules.Core.Domain.ValueObjects.Molecules;
using Molecules.Shared.Constants;

namespace Molecules.Core.Factories.Analysis
{
    public class MoleculesVectorFactory : IMoleculesVectorFactory
    {
        public MoleculeAtomPopulationHomoVector CreateMoleculeAtomPopulationHomoVector(Atom atom, Molecule molecule)
        {
            var retval = new MoleculeAtomPopulationHomoVector($"{molecule.Name}:{atom.Symbol}{atom.Position}");

            retval.Values.LowdinPopulation = atom.LowdinPopulationHOMO.GetValueOrDefault();
            retval.Values.MullikenPopulation = atom.MullikenPopulationHOMO.GetValueOrDefault();
            retval.Values.AtomNumber = atom.Number;



            retval.Data.LowdinPopulation = atom.LowdinPopulationHOMO.GetValueOrDefault();
            retval.Data.MullikenPopulation = atom.MullikenPopulationHOMO.GetValueOrDefault();
            retval.Data.AtomNumber = atom.Number;


            retval.Data.AtomGroup = molecule.AtomGroup(atom);

            return retval;
        }

        public MoleculeAtomPopulationLumoVector CreateMoleculeAtomPopulationLumoVector(Atom atom, Molecule molecule)
        {
            var retval = new MoleculeAtomPopulationLumoVector($"{molecule.Name}:{atom.Symbol}{atom.Position}");

            retval.Values.LowdinPopulation = atom.LowdinPopulationLUMO.GetValueOrDefault();
            retval.Values.MullikenPopulation = atom.MullikenPopulationLUMO.GetValueOrDefault();
            retval.Values.AtomNumber = atom.Number;



            retval.Data.LowdinPopulation = atom.LowdinPopulationLUMO.GetValueOrDefault();
            retval.Data.MullikenPopulation = atom.MullikenPopulationLUMO.GetValueOrDefault();
            retval.Data.AtomNumber = atom.Number;


            retval.Data.AtomGroup = molecule.AtomGroup(atom);

            return retval;
        }

        public MoleculeAtomPopulationVector CreateMoleculeAtomPopulationVector(Atom atom, Molecule molecule)
        {
            var retval = new MoleculeAtomPopulationVector($"{molecule.Name}:{atom.Symbol}{atom.Position}");

            retval.Values.LowdinPopulation = atom.LowdinPopulation.GetValueOrDefault();
            retval.Values.MullikenPopulation = atom.MullikenPopulation.GetValueOrDefault();
            retval.Values.AtomNumber = atom.Number;

            
            
            retval.Data.LowdinPopulation = atom.LowdinPopulation.GetValueOrDefault();
            retval.Data.MullikenPopulation = atom.MullikenPopulation.GetValueOrDefault();
            retval.Data.AtomNumber = atom.Number;


            retval.Data.AtomGroup = molecule.AtomGroup(atom);

            return retval;
        }

        public MoleculesAtomOrbitalPopulationVector CreateMoleculesAtomOrbitalPopulationVector(Atom atom, Molecule molecule)
        {
            var retval = new MoleculesAtomOrbitalPopulationVector($"{molecule.Name}:{atom.Symbol}{atom.Position}");

            retval.Values.LowdinPopulation = atom.LowdinPopulation.GetValueOrDefault();

            retval.Values.LowdinPopulationLumo = atom.LowdinPopulationLUMO.GetValueOrDefault();

            retval.Values.LowdinPopulationHomo = atom.LowdinPopulationHOMO.GetValueOrDefault();
            
            retval.Values.MullikenPopulation = atom.MullikenPopulation.GetValueOrDefault();

            retval.Values.MullikenPopulationLumo = atom.MullikenPopulationLUMO.GetValueOrDefault();

            retval.Values.MullikenPopulationHomo = atom.MullikenPopulationHOMO.GetValueOrDefault();
            
            retval.Values.AtomNumber = atom.Number;
            
            retval.Info.LowdinPopulation = atom.LowdinPopulation.GetValueOrDefault();

            retval.Values.LowdinPopulationLumo = atom.LowdinPopulationLUMO.GetValueOrDefault();

            retval.Values.LowdinPopulationHomo = atom.LowdinPopulationHOMO.GetValueOrDefault();

            retval.Info.MullikenPopulation = atom.MullikenPopulation.GetValueOrDefault();

            retval.Values.MullikenPopulationLumo = atom.MullikenPopulationLUMO.GetValueOrDefault();

            retval.Values.MullikenPopulationHomo = atom.MullikenPopulationHOMO.GetValueOrDefault();

            retval.Info.AtomNumber = atom.Number;

            retval.Info.AtomGroup = molecule.AtomGroup(atom);


            return retval;
        }
    }
}
