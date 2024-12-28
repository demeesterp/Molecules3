using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Orbital;
using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Population;
using Molecules.Core.Domain.ValueObjects.Molecules;

namespace Molecules.Core.Factories.Analysis
{
    public interface IMoleculesVectorFactory
    {
        MoleculeAtomPopulationVector CreateMoleculeAtomPopulationVector(Atom atom, Molecule molecule);


        MoleculesAtomOrbitalPopulationVector CreateMoleculesAtomOrbitalPopulationVector(Atom atom, Molecule molecule);

    }
}
