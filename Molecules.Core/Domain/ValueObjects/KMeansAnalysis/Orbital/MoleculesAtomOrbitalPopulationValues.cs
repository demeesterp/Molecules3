using static Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Orbital.MoleculesAtomOrbitalPopulationConstants;


namespace Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Orbital
{
    public class MoleculesAtomOrbitalPopulationValues
    {
        private double[] values = Enumerable.Repeat(0.0, 100).ToArray();

        public int Dimensions => 7;

        public double AtomNumber
        {
            get => values[AtomNumberPos];
            set => values[AtomNumberPos] = value;
        }

        public double MullikenPopulation
        {
            get => values[MullikenPopulationPos];
            set => values[MullikenPopulationPos] = value;
        }

        public double MullikenPopulationHomo 
        { 
            get => values[MullikenPopulationHomoPos];
            set => values[MullikenPopulationHomoPos] = value; 
        }

        public double MullikenPopulationLumo 
        { 
            get => values[MullikenPopulationLumoPos]; 
            set => values[MullikenPopulationLumoPos] = value; 
        }

        public double LowdinPopulation 
        {
            get => values[LowdinPopulationPos];
            set => values[LowdinPopulationPos] = value;
        }

        public double LowdinPopulationHomo 
        {
            get => values[LowdinPopulationHomoPos];
            set => values[LowdinPopulationHomoPos] = value;
        }

        public double LowdinPopulationLumo 
        {
            get => values[LowdinPopulationLumoPos];
            set => values[LowdinPopulationLumoPos] = value;
        }

        public double this[int index]
        {
            get => values[index];
            set => values[index] = value;
        }

    }
}
