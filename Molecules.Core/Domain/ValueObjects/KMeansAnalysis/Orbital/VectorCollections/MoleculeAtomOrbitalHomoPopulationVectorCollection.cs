using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Base;
using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Orbital.Vectors;

namespace Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Orbital.VectorCollections
{
    public class MoleculeAtomOrbitalHomoPopulationVectorCollection : MoleculesVectorCollection
    {
        public override MoleculesVector CreateEmptyCentroid() => new MoleculeAtomOrbitalHomoPopulationVector(string.Empty, Dimensions);

        public int AtomNumber
        {
            get;
            set;
        }

        public MoleculeAtomOrbitalHomoPopulationVectorCollection(double atomNumber)
        {
            AtomNumber = (int)Math.Round(atomNumber);
        }

        public MoleculeAtomOrbitalHomoPopulationVector AddVector(MoleculeAtomOrbitalHomoPopulationVector toAdd)
        {
            return (MoleculeAtomOrbitalHomoPopulationVector)base.AddVector(toAdd);
        }

        public void AddVectors(IList<MoleculeAtomOrbitalHomoPopulationVector> vectorsToAdd)
        {
            AddVectors(vectorsToAdd.Cast<MoleculesVector>().ToList());
        }


        public override void Normalize()
        {
            Dictionary<int, double> maxValues = new Dictionary<int, double>();
            for (int dim = 0; dim < Dimensions; ++dim)
            {
                maxValues.Add(dim, Vectors.Max(x => x.GetValue(dim)));
            }

            Dictionary<int, double> minValues = new Dictionary<int, double>();
            for (int dim = 0; dim < Dimensions; ++dim)
            {
                minValues.Add(dim, Vectors.Min(x => x.GetValue(dim)));
            }

            for (int dim = 0; dim < Dimensions; ++dim)
            {
                foreach (MoleculeAtomOrbitalHomoPopulationVector v in Vectors)
                {
                    var minMaxDiff = maxValues[dim] - minValues[dim];
                    if (minMaxDiff > 0)
                    {
                        v.SetValue(dim, v.GetValue(dim) - minValues[dim] / maxValues[dim] - minValues[dim]);
                    }
                }
            }
        }
    }
}
