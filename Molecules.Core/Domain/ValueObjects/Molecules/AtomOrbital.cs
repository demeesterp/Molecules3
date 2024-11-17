using System.Text.Json.Serialization;

namespace Molecules.Core.Domain.ValueObjects.Molecules
{
    public class AtomOrbital
    {
        public int Position { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public double? MullikenPopulation { get; set; }
        public double? MullikenPopulationMinus1 { get; set; }
        public double? MullikenPopulationPlus1 { get; set; }
        public double? LowdinPopulation { get; set; }
        public double? LowdinPopulationMinus1 { get; set; }
        public double? LowdinPopulationPlus1 { get; set; }

        [JsonIgnore]
        public double? MullikenPopulationLumo => MullikenPopulationPlus1 - MullikenPopulation;
        [JsonIgnore]
        public double? MullikenPopulationHomo => MullikenPopulation - MullikenPopulationMinus1;
        [JsonIgnore]
        public double? LowdinPopulationLumo => LowdinPopulationPlus1 - LowdinPopulation;
        [JsonIgnore]
        public double? LowdinPopulationHomo => LowdinPopulation - LowdinPopulationMinus1;
    }
}
