using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Population;
using System.Text;

namespace Molecules.Core.Domain.ValueObjects.Analysis.Population
{
    public class MoleculeAtomPopulationAnalysisResult
    {
        public List<MoleculeAtomPopulationCategory> Results = new List<MoleculeAtomPopulationCategory>();

        public string GetReport()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine($"ClusterLabel;Atom;Name;LowdinPopulation;MullikenPopulation;");
            foreach (var item in Results)
            {
                foreach(var v in item.Items)
                {
                    result.AppendLine($"{item.ClusterLabel};{item.Atom}{v.AtomNumber};{v.Name};{v.LowdinPopulation};{v.MullikenPopulation};");
                }
            }
            return result.ToString();
        }


    }
}
