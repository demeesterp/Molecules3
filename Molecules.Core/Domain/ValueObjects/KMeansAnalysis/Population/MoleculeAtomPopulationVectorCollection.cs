using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Base;

namespace Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Population
{
    public class MoleculeAtomPopulationVectorCollection : MoleculesVectorCollection
    {
        public override MoleculesVector CreateEmptyCentroid()
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

        public override void Normalize()
        {
            MoleculeAtomPopulationValues values = new MoleculeAtomPopulationValues();
            foreach(MoleculeAtomPopulationVector v in Vectors)
            {
                values.MullikenPopulation += v.Values.MullikenPopulation;
                values.LowdinPopulation += v.Values.LowdinPopulation;
            }
            values.MullikenPopulation /= Vectors.Count;
            values.LowdinPopulation /= Vectors.Count;
        }
    }
}
