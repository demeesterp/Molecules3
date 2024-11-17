using Molecules.Shared;

namespace Molecules.Core.Domain.ValueObjects.Reports
{
    public record ConfigurationReportItem(string Symbol, double? Population, double? PopulationFraction)
    {
        public override string ToString()
        {
            return Symbol + "(" + StringConversion.ToString(PopulationFraction, "0.00") + ")";
        }
    }
}
