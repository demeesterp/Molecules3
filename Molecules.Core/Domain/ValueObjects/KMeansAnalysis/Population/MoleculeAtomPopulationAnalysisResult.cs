using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Population;
using System.Text;

namespace Molecules.Core.Domain.ValueObjects.Analysis.Population
{
    public class MoleculeAtomPopulationAnalysisResult
    {
        public List<MoleculeAtomPopulationCategory> Categories = new List<MoleculeAtomPopulationCategory>();

        public string GetReport()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine($"ClusterLabel;AtomGroup;Atom;Name;LowdinPopulation;MullikenPopulation;");
            foreach (var category in Categories)
            {
                foreach(var v in category)
                {
                    result.AppendLine($"{category.Label};{v.Data.AtomGroup};{category.Atom}{v.Data.AtomNumber};{v.Name};{v.Data.LowdinPopulation};{v.Data.MullikenPopulation};");
                }
            }
            return result.ToString();
        }


    }
}
