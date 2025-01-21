using Molecules.Shared;
using MoleculesWebApp.Client.Data.ServiceAgents.Molecules.Report;

namespace MoleculesWebApp.Client.Data.Model
{
    public class ConfigurationReportItemModel
    {
        public ConfigurationReportItemModel(ConfigurationReportItem configurationReportItem)
        {
            Symbol = configurationReportItem.Symbol;
            Population = configurationReportItem.Population;
            PopulationFraction = configurationReportItem.PopulationFraction;
        }

        public string Symbol { get; set; } = "";

        public decimal? Population { get; set; }

        public decimal? PopulationFraction { get; set; }


        public override string ToString()
        {
            return Symbol + "(" + StringConversion.ToString(Population, "0.00") + ")";
        }

        public decimal ColorFraction
        {
            get
            {
                return Math.Min(0.8m, Math.Abs(Population ?? 0));
            }
        }
    }
}
