using Molecules.Core.Domain.ValueObjects.Analysis.Population;
using Molecules.Core.Domain.ValueObjects.Molecules;

namespace Molecules.Core.Factories.Analysis
{
    public class MoleculesVectorFactory : IMoleculesVectorFactory
    {                
        public MoleculeAtomPopulationVector CreateMoleculeAtomPopulationVector(Atom atom, string moleculeName)
        {
            return new MoleculeAtomPopulationVector($"{moleculeName}:{atom.Symbol}{atom.Position}")
            {
                AtomNumber = atom.Number,
                LowdinPopulation = atom.LowdinPopulation.GetValueOrDefault(),
                MullikenPopulation = atom.MullikenPopulation.GetValueOrDefault()
            };
        }
    }
}
