namespace Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Orbital.Vectors
{
    public class MoleculeAtomOrbitalPopulationInfo
    {
        public double AtomNumber { get; set; }

        public int AtomPosition { get; set; }

        public string? AtomGroup { get; set; }

        public List<MoleculeAtomOrbitalPopulationValueItem> Items = new ();
      
    }
}
