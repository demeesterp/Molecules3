namespace Molecules.Core.Domain.ValueObjects.KMeansAnalysis
{
    public class MoleculeAtomClusteringInput
    {
        public MoleculeAtomClusteringInput()
        {
            Items = [
                new () { Atom = AtomData.AtomsEnum.H, NbrOfClusters = 3},
                new () { Atom = AtomData.AtomsEnum.C, NbrOfClusters = 3},
                new () { Atom = AtomData.AtomsEnum.O, NbrOfClusters = 3},
                new () { Atom = AtomData.AtomsEnum.N, NbrOfClusters = 3},
                new () { Atom = AtomData.AtomsEnum.S, NbrOfClusters = 3}
                ];
        }
        public KMeansVectorTypeEnum Type { get; set; }
        public List<MoleculeAtomClusteringInputItem> Items { get; set; }
    }
}
