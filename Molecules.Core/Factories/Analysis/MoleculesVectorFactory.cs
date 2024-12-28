using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Orbital;
using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Population;
using Molecules.Core.Domain.ValueObjects.Molecules;
using Molecules.Shared.Constants;

namespace Molecules.Core.Factories.Analysis
{
    public class MoleculesVectorFactory : IMoleculesVectorFactory
    {                
        public MoleculeAtomPopulationVector CreateMoleculeAtomPopulationVector(Atom atom, Molecule molecule)
        {
            var retval = new MoleculeAtomPopulationVector($"{molecule.Name}:{atom.Symbol}{atom.Position}");

            retval.Values.LowdinPopulation = atom.LowdinPopulation.GetValueOrDefault();
            retval.Values.MullikenPopulation = atom.MullikenPopulation.GetValueOrDefault();
            retval.Values.AtomNumber = atom.Number;

            
            
            retval.Data.LowdinPopulation = atom.LowdinPopulation.GetValueOrDefault();
            retval.Data.MullikenPopulation = atom.MullikenPopulation.GetValueOrDefault();
            retval.Data.AtomNumber = atom.Number;


            retval.Data.AtomGroup = $"{atom.Symbol}{atom.Number}({atom.Position})";
            foreach(var grpItem in  molecule.Bonds.Where(b => 
                                                            b.OverlapPopulation >= MoleculesConstants.BondThreshold
                                                            && 
                                                            (
                                                                b.Atom1Position == atom.Position 
                                                                || 
                                                                b.Atom2Position == atom.Position)
                                                            ))
            {            
                if ( grpItem.Atom1Position == atom.Position)
                {
                    var correspondingAtom = molecule.Atoms.First(x => x.Position == grpItem.Atom2Position);
                    retval.Data.AtomGroup += $"{grpItem.BondSymbol}{correspondingAtom.Symbol}{correspondingAtom.Position}";
                }
                
                if ( grpItem.Atom2Position == atom.Position)
                {
                    var correspondingAtom = molecule.Atoms.First(x => x.Position == grpItem.Atom1Position);
                    retval.Data.AtomGroup += $"{grpItem.BondSymbol}{correspondingAtom.Symbol}{correspondingAtom.Position}";
                }
            }

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

            retval.Info.AtomGroup = $"{atom.Symbol}{atom.Number}({atom.Position})";
            
            foreach (var grpItem in molecule.Bonds.Where(b => b.OverlapPopulation >= MoleculesConstants.BondThreshold
                                                                &&
                                                                (
                                                                    b.Atom1Position == atom.Position
                                                                    ||
                                                                    b.Atom2Position == atom.Position)
                                                                ))
            {
                if (grpItem.Atom1Position == atom.Position)
                {
                    var correspondingAtom = molecule.Atoms.First(x => x.Position == grpItem.Atom2Position);
                    retval.Info.AtomGroup += $"{grpItem.BondSymbol}{correspondingAtom.Symbol}{correspondingAtom.Position}";
                }

                if (grpItem.Atom2Position == atom.Position)
                {
                    var correspondingAtom = molecule.Atoms.First(x => x.Position == grpItem.Atom1Position);
                    retval.Info.AtomGroup += $"{grpItem.BondSymbol}{correspondingAtom.Symbol}{correspondingAtom.Position}";
                }
            }


            return retval;
        }
    }
}
