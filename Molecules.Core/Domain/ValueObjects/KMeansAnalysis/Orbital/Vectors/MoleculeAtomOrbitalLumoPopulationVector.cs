namespace Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Orbital.Vectors
{
    public class MoleculeAtomOrbitalLumoPopulationVector : MoleculeAtomOrbitalPopulationVector
    {
        public MoleculeAtomOrbitalLumoPopulationVector(string name) : base(name) {}

        public MoleculeAtomOrbitalLumoPopulationVector(string name, int dimensions) : base(name, dimensions) {}
    }
}
