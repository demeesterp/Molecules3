

namespace Molecules.Core.Domain.ValueObjects.Analysis.Population
{
    public class MoleculeAtomPopulationVectorCollection : MoleculesVectorCollection
    {
        protected override MoleculesVector CreateEmptyCentroid()
        {
            return new MoleculeAtomPopulationVector(string.Empty);
        }

        public int AtomNumber { get; set; }


        public MoleculeAtomPopulationVectorCollection(double atomNumber)
        {
            AtomNumber = (int)Math.Round(atomNumber);
        }

        public MoleculeAtomPopulationVector AddVector(MoleculeAtomPopulationVector toAdd)
        {
            return (MoleculeAtomPopulationVector)base.AddVector(toAdd);
        }

        public void AddVectors(IList<MoleculeAtomPopulationVector> vectorsToAdd)
        {
            AddVectors(vectorsToAdd.Cast<MoleculesVector>().ToList());
        }

        protected override void Normalize()
        {
           
        }
    }
}
