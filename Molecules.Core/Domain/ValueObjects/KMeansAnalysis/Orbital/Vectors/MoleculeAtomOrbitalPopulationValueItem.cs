namespace Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Orbital.Vectors
{
    public class MoleculeAtomOrbitalPopulationValueItem
    {
        public int Shell { get; set; }

        public int VectorPositionMulliken { get; set; }

        public int VectorPositionLowdin { get; set; }

        public string Symbol { get; set; } = string.Empty;

        public double LowdinPopulation { get; set; }

        public double MullikenPopulation { get; set; }


    }
}
