using Molecules.Core.Domain.ValueObjects.AtomData;
using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Base;
using System.Collections;

namespace Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Population
{
    public class MoleculeAtomPopulationCategory : MoleculesCluster<MoleculeAtomPopulationVector>
    {      
        public MoleculeAtomPopulationCategory(MoleculesCluster<MoleculeAtomPopulationVector> cluster): base(cluster) { }

        public MoleculeAtomPopulationCategory(AtomsEnum atom, MoleculesCluster<MoleculeAtomPopulationVector> cluster) : this(cluster)
        {
            Atom = atom;
        }

        public AtomsEnum Atom { get; set; }

       
    }


}
