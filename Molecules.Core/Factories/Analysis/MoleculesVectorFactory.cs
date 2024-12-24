using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Population;
using Molecules.Core.Domain.ValueObjects.Molecules;

namespace Molecules.Core.Factories.Analysis
{
    public class MoleculesVectorFactory : IMoleculesVectorFactory
    {                
        public MoleculeAtomPopulationVector CreateMoleculeAtomPopulationVector(Atom atom, string moleculeName)
        {
            var retval = new MoleculeAtomPopulationVector($"{moleculeName}:{atom.Symbol}{atom.Position}");

            retval.Values.LowdinPopulation = atom.LowdinPopulation.GetValueOrDefault();
            retval.Values.MullikenPopulation = atom.MullikenPopulation.GetValueOrDefault();
            retval.Values.AtomNumber = atom.Number;

            retval.Data.LowdinPopulation = atom.LowdinPopulation.GetValueOrDefault();
            retval.Data.MullikenPopulation = atom.MullikenPopulation.GetValueOrDefault();
            retval.Data.AtomNumber = atom.Number;

            return retval;
        }
    }
}
