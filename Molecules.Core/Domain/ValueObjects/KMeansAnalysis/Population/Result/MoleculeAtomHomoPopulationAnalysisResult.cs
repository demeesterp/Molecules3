using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Base.Result;
using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Population.Vectors;
using System.Text;

namespace Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Population.Result
{
    public class MoleculeAtomHomoPopulationAnalysisResult : MoleculeAtomAnalysisResult<MoleculeAtomHomoPopulationVector>, IMoleculeAnalysisResult
    {
        public string GetReport()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine($"ClusterLabel;Atom;AtomPosition;MoleculeName;AtomGroup;LowdinHomoPopulation;MullikenHomoPopulation;");
            foreach (var category in Categories)
            {
                foreach (var v in category)
                {
                    result.AppendLine($"{category.Label};{category.Atom};{v.Info.AtomPosition};{v.Name};{v.Info.AtomGroup};{v.Info.LowdinPopulation};{v.Info.MullikenPopulation};");
                }
            }
            return result.ToString();
        }
    }
}
