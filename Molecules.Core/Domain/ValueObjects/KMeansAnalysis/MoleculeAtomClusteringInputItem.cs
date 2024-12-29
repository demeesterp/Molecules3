using Molecules.Core.Domain.ValueObjects.AtomData;

namespace Molecules.Core.Domain.ValueObjects.KMeansAnalysis
{
    public record MoleculeAtomClusteringInputItem
    {
        public AtomsEnum Atom { get; set; } = AtomsEnum.H;


        public int NbrOfClusters { get; set; }


    }
}
