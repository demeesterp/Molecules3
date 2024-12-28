using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Base;

namespace Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Population
{
    public class MoleculeAtomPopulationHomoVectorCollection : MoleculesVectorCollection
    {
        public override MoleculesVector CreateEmptyCentroid()
        {
            return new MoleculeAtomPopulationHomoVector(string.Empty);
        }

        public int AtomNumber { get; set; }


        public MoleculeAtomPopulationHomoVectorCollection(double atomNumber)
        {
            AtomNumber = (int)Math.Round(atomNumber);
        }

        public MoleculeAtomPopulationHomoVector AddVector(MoleculeAtomPopulationHomoVector toAdd)
        {
            return (MoleculeAtomPopulationHomoVector)base.AddVector(toAdd);
        }

        public void AddVectors(IList<MoleculeAtomPopulationHomoVector> vectorsToAdd)
        {
            AddVectors(vectorsToAdd.Cast<MoleculesVector>().ToList());
        }

        public override void Normalize()
        {
            MoleculeAtomPopulationValues values = new MoleculeAtomPopulationValues();
            foreach (MoleculeAtomPopulationVector v in Vectors)
            {
                values.MullikenPopulation += v.Values.MullikenPopulation;
                values.LowdinPopulation += v.Values.LowdinPopulation;
            }
            values.MullikenPopulation /= Vectors.Count;
            values.LowdinPopulation /= Vectors.Count;
        }
    }
}
