using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Orbital.Vectors;
using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Population.Vectors;
using Molecules.Core.Domain.ValueObjects.Molecules;
using Molecules.Shared.Constants;

namespace Molecules.Core.Factories.Analysis
{
    public class MoleculesVectorFactory : IMoleculesVectorFactory
    {
        public MoleculeAtomHomoPopulationVector CreateMoleculeAtomPopulationHomoVector(Atom atom, Molecule molecule)
        {
            var retval = new MoleculeAtomHomoPopulationVector($"{molecule.Name}:{atom.Symbol}{atom.Position}");

            retval.Values.LowdinPopulation = atom.LowdinPopulationHOMO.GetValueOrDefault();
            retval.Values.MullikenPopulation = atom.MullikenPopulationHOMO.GetValueOrDefault();
            retval.Values.AtomNumber = atom.Number;



            retval.Info.LowdinPopulation = atom.LowdinPopulationHOMO.GetValueOrDefault();
            retval.Info.MullikenPopulation = atom.MullikenPopulationHOMO.GetValueOrDefault();
            retval.Info.AtomNumber = atom.Number;
            retval.Info.AtomPosition = atom.Position;


            retval.Info.AtomGroup = molecule.AtomGroup(atom);

            return retval;
        }

        public MoleculeAtomLumoPopulationVector CreateMoleculeAtomPopulationLumoVector(Atom atom, Molecule molecule)
        {
            var retval = new MoleculeAtomLumoPopulationVector($"{molecule.Name}:{atom.Symbol}{atom.Position}");

            retval.Values.LowdinPopulation = atom.LowdinPopulationLUMO.GetValueOrDefault();
            retval.Values.MullikenPopulation = atom.MullikenPopulationLUMO.GetValueOrDefault();
            retval.Values.AtomNumber = atom.Number;



            retval.Info.LowdinPopulation = atom.LowdinPopulationLUMO.GetValueOrDefault();
            retval.Info.MullikenPopulation = atom.MullikenPopulationLUMO.GetValueOrDefault();
            retval.Info.AtomNumber = atom.Number;
            retval.Info.AtomPosition = atom.Position;


            retval.Info.AtomGroup = molecule.AtomGroup(atom);

            return retval;
        }

        public MoleculeAtomPopulationVector CreateMoleculeAtomPopulationVector(Atom atom, Molecule molecule)
        {
            var retval = new MoleculeAtomPopulationVector($"{molecule.Name}:{atom.Symbol}{atom.Position}");

            retval.Values.LowdinPopulation = atom.LowdinPopulation.GetValueOrDefault();
            retval.Values.MullikenPopulation = atom.MullikenPopulation.GetValueOrDefault();
            retval.Values.AtomNumber = atom.Number;

            
            
            retval.Info.LowdinPopulation = atom.LowdinPopulation.GetValueOrDefault();
            retval.Info.MullikenPopulation = atom.MullikenPopulation.GetValueOrDefault();
            retval.Info.AtomNumber = atom.Number;
            retval.Info.AtomPosition = atom.Position;


            retval.Info.AtomGroup = molecule.AtomGroup(atom);

            return retval;
        }

        public MoleculeAtomOrbitalPopulationVector CreateMoleculesAtomOrbitalPopulationVector(Atom atom, Molecule molecule)
        {
            var retval = new MoleculeAtomOrbitalPopulationVector($"{molecule.Name}:{atom.Symbol}{atom.Position}");
            retval.Info.AtomNumber = atom.Number;
            retval.Info.AtomPosition = atom.Position;
            retval.Info.AtomGroup = molecule.AtomGroup(atom);
            retval.Values.AtomNumber = atom.Number;
            int count = 0;
            foreach(var atomOrbital in atom.Orbitals)
            {

                var item = new MoleculeAtomOrbitalPopulationValueItem()
                {
                    VectorPositionLowdin = count++,
                    VectorPositionMulliken = count++,
                    LowdinPopulation = atomOrbital.LowdinPopulation.GetValueOrDefault(),
                    MullikenPopulation = atomOrbital.MullikenPopulation.GetValueOrDefault(),
                    Symbol = atomOrbital.Symbol,
                    Shell = atom.OrbitalShell(atomOrbital)
                };

                retval.Values.AppendItem(item);
                retval.Info.Items.Add(item);
            }
            return retval;
        }

        public MoleculeAtomOrbitalHomoPopulationVector CreateMoleculesAtomOrbitalHomoPopulationVector(Atom atom, Molecule molecule)
        {
            var retval = new MoleculeAtomOrbitalHomoPopulationVector($"{molecule.Name}:{atom.Symbol}{atom.Position}");
            retval.Info.AtomNumber = atom.Number;
            retval.Info.AtomPosition = atom.Position;
            retval.Info.AtomGroup = molecule.AtomGroup(atom);
            retval.Values.AtomNumber = atom.Number;
            int count = 0;
            foreach (var atomOrbital in atom.Orbitals)
            {

                var item = new MoleculeAtomOrbitalPopulationValueItem()
                {
                    VectorPositionLowdin = count++,
                    VectorPositionMulliken = count++,
                    LowdinPopulation = atomOrbital.LowdinPopulationHomo.GetValueOrDefault(),
                    MullikenPopulation = atomOrbital.MullikenPopulationHomo.GetValueOrDefault(),
                    Symbol = atomOrbital.Symbol,
                    Shell = atom.OrbitalShell(atomOrbital)
                };

                retval.Values.AppendItem(item);
                retval.Info.Items.Add(item);
            }
            return retval;
        }

        public MoleculeAtomOrbitalLumoPopulationVector CreateMoleculesAtomOrbitalLumoPopulationVector(Atom atom, Molecule molecule)
        {
            var retval = new MoleculeAtomOrbitalLumoPopulationVector($"{molecule.Name}:{atom.Symbol}{atom.Position}");
            retval.Info.AtomNumber = atom.Number;
            retval.Info.AtomPosition = atom.Position;
            retval.Info.AtomGroup = molecule.AtomGroup(atom);
            retval.Values.AtomNumber = atom.Number;
            int count = 0;
            foreach (var atomOrbital in atom.Orbitals)
            {

                var item = new MoleculeAtomOrbitalPopulationValueItem()
                {
                    VectorPositionLowdin = count++,
                    VectorPositionMulliken = count++,
                    LowdinPopulation = atomOrbital.LowdinPopulationLumo.GetValueOrDefault(),
                    MullikenPopulation = atomOrbital.MullikenPopulationLumo.GetValueOrDefault(),
                    Symbol = atomOrbital.Symbol,
                    Shell = atom.OrbitalShell(atomOrbital)
                };

                retval.Values.AppendItem(item);
                retval.Info.Items.Add(item);
            }
            return retval;
        }
    }
}
