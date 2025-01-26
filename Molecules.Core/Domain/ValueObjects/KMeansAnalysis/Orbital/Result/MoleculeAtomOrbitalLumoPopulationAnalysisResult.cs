using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Base.Result;
using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Orbital.Vectors;
using System.Text;

namespace Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Orbital.Result
{
    public class MoleculeAtomOrbitalLumoPopulationAnalysisResult : MoleculeAtomAnalysisResult<MoleculeAtomOrbitalLumoPopulationVector>, IMoleculeAnalysisResult
    {

        public string GetReport()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine($"ClusterLabel;AtomGroup;Atom;Name;");
            foreach (var category in Categories)
            {
                foreach (var v in category)
                {
                    result.AppendLine($"{category.Label};{v.Info.AtomGroup};{category.Atom}{v.Values.AtomNumber};{v.Name};");
                }
            }
            return result.ToString();
        }


    }
}
