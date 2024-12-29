using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Orbital.Vectors;
using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Population.Vectors;
using Molecules.Core.Domain.ValueObjects.Molecules;

namespace Molecules.Core.Factories.Analysis
{
    public interface IMoleculesVectorFactory
    {
        MoleculeAtomPopulationVector CreateMoleculeAtomPopulationVector(Atom atom, Molecule molecule);

        MoleculeAtomPopulationHomoVector CreateMoleculeAtomPopulationHomoVector(Atom atom, Molecule molecule);

        MoleculeAtomPopulationLumoVector CreateMoleculeAtomPopulationLumoVector(Atom atom, Molecule molecule);

        MoleculeAtomOrbitalPopulationVector CreateMoleculesAtomOrbitalPopulationVector(Atom atom, Molecule molecule);

    }
}
