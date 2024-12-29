namespace Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Population.Vectors
{
    public record MoleculeAtomPopulationInfo
    {
        public double MullikenPopulation { get; set; }

        public double LowdinPopulation { get; set; }

        public double AtomNumber { get; set; }

        public string AtomGroup { get; set; } = string.Empty;
    }

}
