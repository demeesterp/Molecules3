using Molecules.Core.Domain.ValueObjects.Molecules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Molecules.Core.Factories.CalcParsers
{
    internal class LewisLUMOPopulationAnalysisParser : UHFPopulationAnalysisParser
    {
        protected override PopulationAnalysisType GetPopulationStatus()
        {
            return PopulationAnalysisType.lewisLUMO;
        }

        internal static void GetPopulation(List<string> fileLines, Molecule molecule)
        {
            LewisLUMOPopulationAnalysisParser parser = new();
            parser.Parse(fileLines, molecule);
        }
    }
}
