namespace Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Orbital
{
    public class MoleculesAtomOrbitalPopulationInfo
    {
        public double AtomNumber { get; set; }

        public string? AtomGroup { get; set; }

        public double MullikenPopulation { get; set; }

        public double MullikenPopulationHomo { get; set; }

        public double MullikenPopulationLumo { get; set; }


        public double LowdinPopulation { get; set; }

        public double LowdinPopulationHomo { get; set; }

        public double LowdinPopulationLumo { get; set; }
    }
}
