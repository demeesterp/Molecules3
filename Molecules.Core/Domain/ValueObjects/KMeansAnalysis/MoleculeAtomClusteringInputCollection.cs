namespace Molecules.Core.Domain.ValueObjects.KMeansAnalysis
{
    public class MoleculeAtomClusteringInputCollection
    {

        public List<MoleculeAtomClusteringInput> ClusterList { get; set; } = new List<MoleculeAtomClusteringInput>();

        public static MoleculeAtomClusteringInputCollection Default
        {
            get
            {
                var result = new MoleculeAtomClusteringInputCollection();
                foreach (var vectorType in Enum.GetValues<KMeansVectorTypeEnum>())
                {
                    result.ClusterList.Add(new MoleculeAtomClusteringInput()
                    {
                        Type = vectorType,
                        Active = false
                    });
                }
                return result;
            }
        }


    }
}
