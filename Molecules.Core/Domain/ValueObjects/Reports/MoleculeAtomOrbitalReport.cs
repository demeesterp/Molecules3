using Molecules.Shared;
using System.Text.Json.Serialization;
using System.Text;

namespace Molecules.Core.Domain.ValueObjects.Reports
{
    public class MoleculeAtomOrbitalReport
    {
        public string MoleculeName { get; set; } = "";

        public string AtomID { get; set; } = "";

        public decimal? MullikenPopulation { get; set; }

        public string ElectronConfiguration
        {
            get
            {
                StringBuilder sb = new();
                OrbitalReport.ForEach(orbital => sb.Append($"{orbital.OrbitalSymbol}({StringConversion.ToString(orbital.PopulationFraction, "0.00")})"));
                return sb.ToString();
            }
        }

        public string ElectronConfigurationHOMO
        {
            get
            {
                StringBuilder sb = new();
                OrbitalReport.ForEach(orbital => sb.Append($"{orbital.OrbitalSymbol}({StringConversion.ToString(orbital.PopulationFractionHOMO, "0.00")})"));
                return sb.ToString();
            }
        }

        public string ElectronConfigurationLUMO
        {
            get
            {
                StringBuilder sb = new();
                OrbitalReport.ForEach(orbital => sb.Append($"{orbital.OrbitalSymbol}({StringConversion.ToString(orbital.PopulationFractionLUMO, "0.00")})"));
                return sb.ToString();
            }
        }

        [JsonIgnore]
        public List<AtomOrbitalReport> OrbitalReport { get; set; } = [];
    }
}
