using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Base;
using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Population.Vectors;

namespace Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Population.VectorCollection
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
            Dictionary<int, double> maxValues = new Dictionary<int, double>();
            for (int cnt = 0; cnt < Dimensions; ++cnt)
            {
                maxValues.Add(cnt, Vectors.Max(x => x.GetValue(cnt)));
            }

            Dictionary<int, double> minValues = new Dictionary<int, double>();
            for (int cnt = 0; cnt < Dimensions; ++cnt)
            {
                minValues.Add(cnt, Vectors.Min(x => x.GetValue(cnt)));
            }

            for (int cnt = 0; cnt < Dimensions; ++cnt)
            {
                foreach (MoleculeAtomPopulationHomoVector v in Vectors)
                {
                    var minMaxDiff = maxValues[cnt] - minValues[cnt];
                    if (minMaxDiff > 0)
                    {
                        v.SetValue(cnt, v.GetValue(cnt) - minValues[cnt] / maxValues[cnt] - minValues[cnt]);
                    }
                }
            }
        }
    }
}
