using Molecules.Core.Domain.ValueObjects.Molecules;

namespace Molecules.Core.Factories.CalcParsers
{
    internal class LewisHOMOPopulationAnalysisParser : UHFPopulationAnalysisParser
    {
        protected override PopulationAnalysisType GetPopulationStatus()
        {
            return PopulationAnalysisType.lewisHOMO;
        }

        internal static void GetPopulation(List<string> fileLines, Molecule molecule)
        {
            LewisHOMOPopulationAnalysisParser parser = new();
            parser.Parse(fileLines, molecule);
        }
    }
}
