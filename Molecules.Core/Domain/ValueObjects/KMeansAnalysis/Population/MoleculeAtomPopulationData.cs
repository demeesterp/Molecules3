namespace Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Population
{
    public record MoleculeAtomPopulationData
    {
        public double MullikenPopulation { get; set; }

        public double LowdinPopulation { get; set; }

        public double AtomNumber { get; set; }
    }

}
