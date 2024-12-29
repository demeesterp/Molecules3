namespace Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Base.Result
{
    public class MoleculeAtomAnalysisResult<Type>
    {
        public List<MoleculeAtomCategory<Type>> Categories = new List<MoleculeAtomCategory<Type>>();
    }
}
