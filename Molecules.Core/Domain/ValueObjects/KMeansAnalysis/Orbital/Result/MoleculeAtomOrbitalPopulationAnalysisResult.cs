using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Base.Result;
using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Orbital.Vectors;
using System.Text;

namespace Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Orbital.Result
{
    public class MoleculeAtomOrbitalPopulationAnalysisResult : MoleculeAtomAnalysisResult<MoleculeAtomOrbitalPopulationVector>, IMoleculeAnalysisResult
    {
        public string GetReport()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine($"ClusterLabel;Atom;AtomPosition;MoleculeName;AtomGroup");
            foreach (var category in Categories)
            {
                foreach (var vector in category)
                {
                    result.AppendLine($"{category.Label};{category.Atom};{vector.Info.AtomPosition};{vector.Name};{vector.Info.AtomGroup}");
                }
            }
            return result.ToString();
        }
    }
}
