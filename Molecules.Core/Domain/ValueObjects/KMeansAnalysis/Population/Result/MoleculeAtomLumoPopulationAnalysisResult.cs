using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Base.Result;
using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Population.Vectors;
using System.Text;

namespace Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Population.Result
{
    public class MoleculeAtomLumoPopulationAnalysisResult : MoleculeAtomAnalysisResult<MoleculeAtomLumoPopulationVector>, IMoleculeAnalysisResult
    {
        public string GetReport()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine($"ClusterLabel;AtomGroup;Atom;Name;LowdinLumoPopulation;MullikenLumoPopulation;");
            foreach (var category in Categories)
            {
                foreach (var v in category)
                {
                    result.AppendLine($"{category.Label};{v.Data.AtomGroup};{category.Atom}{v.Data.AtomNumber};{v.Name};{v.Data.LowdinPopulation};{v.Data.MullikenPopulation};");
                }
            }
            return result.ToString();
        }
    }
}
