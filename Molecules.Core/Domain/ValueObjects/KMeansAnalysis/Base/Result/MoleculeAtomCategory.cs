using Molecules.Core.Domain.ValueObjects.AtomData;
using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Base;

namespace Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Base.Result
{
    public class MoleculeAtomCategory<Type> : MoleculesCluster<Type>
    {
        public MoleculeAtomCategory(MoleculesCluster<Type> cluster) : base(cluster) { }

        public MoleculeAtomCategory(AtomsEnum atom, MoleculesCluster<Type> cluster) : this(cluster)
        {
            Atom = atom;
        }

        public AtomsEnum Atom { get; set; }


    }


}
