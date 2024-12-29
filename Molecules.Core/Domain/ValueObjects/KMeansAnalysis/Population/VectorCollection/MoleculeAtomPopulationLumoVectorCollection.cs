using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Base;
using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Population.Vectors;

namespace Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Population.VectorCollection
{
    public class MoleculeAtomPopulationLumoVectorCollection : MoleculesVectorCollection
    {
        public override MoleculesVector CreateEmptyCentroid()
        {
            return new MoleculeAtomPopulationLumoVector(string.Empty);
        }

        public int AtomNumber { get; set; }


        public MoleculeAtomPopulationLumoVectorCollection(double atomNumber)
        {
            AtomNumber = (int)Math.Round(atomNumber);
        }

        public MoleculeAtomPopulationLumoVector AddVector(MoleculeAtomPopulationLumoVector toAdd)
        {
            return (MoleculeAtomPopulationLumoVector)base.AddVector(toAdd);
        }

        public void AddVectors(IList<MoleculeAtomPopulationLumoVector> vectorsToAdd)
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
