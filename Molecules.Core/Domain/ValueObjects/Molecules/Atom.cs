using Molecules.Core.Domain.ValueObjects.AtomData;
using System.Text.Json.Serialization;

namespace Molecules.Core.Domain.ValueObjects.Molecules
{
    public class Atom
    {
        public int Position { get; set; }

        public int Number { get; set; }

        public string Symbol { get; set; } = string.Empty;

        public int? AtomicWeight { get; set; }

        public AtomProperties? Info { get; set; }

        public double? PosX { get; set; }

        public double? PosY { get; set; }

        public double? PosZ { get; set; }

        public double? Radius { get; set; }

        public double? MullikenPopulation { get; set; }

        public double? MullikenPopulationMinus1 { get; set; }

        public double? MullikenPopulationPlus1 { get; set; }

        [JsonIgnore]
        public double? MullikenPopulationLUMO => MullikenPopulationPlus1 - MullikenPopulation;

        [JsonIgnore]
        public double? MullikenPopulationHOMO => MullikenPopulation - MullikenPopulationMinus1;


        public double? LowdinPopulation { get; set; }

        public double? LowdinPopulationMinus1 { get; set; }

        public double? LowdinPopulationPlus1 { get; set; }

        [JsonIgnore]
        public double? LowdinPopulationLUMO => LowdinPopulationPlus1 - LowdinPopulation;

        [JsonIgnore]
        public double? LowdinPopulationHOMO => LowdinPopulation - LowdinPopulationMinus1;

        public double? CHelpGCharge { get; set; }

        public double? ConnollyCharge { get; set; }

        public double? GeoDiscCharge { get; set; }

        public List<AtomOrbital> Orbitals { get; set; } = [];

        public List<Bond> Bonds { get; set; } = [];
    }
}
