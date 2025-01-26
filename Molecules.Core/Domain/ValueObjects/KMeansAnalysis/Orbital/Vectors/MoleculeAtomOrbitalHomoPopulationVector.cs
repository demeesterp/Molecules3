namespace Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Orbital.Vectors
{
    public class MoleculeAtomOrbitalHomoPopulationVector : MoleculeAtomOrbitalPopulationVector
    {
        public MoleculeAtomOrbitalHomoPopulationVector(string name) : base(name) { }

        public MoleculeAtomOrbitalHomoPopulationVector(string name, int dimensions): base(name, dimensions) { }
    }
}
