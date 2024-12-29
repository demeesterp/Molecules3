using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Base;
using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Orbital.Vectors;
using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Population.Vectors;

namespace Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Orbital.VectorCollections
{
    public class MoleculeAtomOrbitalPopulationVectorCollection : MoleculesVectorCollection
    {
        public override MoleculesVector CreateEmptyCentroid() => new MoleculeAtomOrbitalPopulationVector(string.Empty, Dimensions);
        public int AtomNumber
        {
            get;
            set;
        }

        public MoleculeAtomOrbitalPopulationVectorCollection(double atomNumber)
        {
            AtomNumber = (int)Math.Round(atomNumber);
        }

        public MoleculeAtomOrbitalPopulationVector AddVector(MoleculeAtomOrbitalPopulationVector toAdd)
        {
            return (MoleculeAtomOrbitalPopulationVector)base.AddVector(toAdd);
        }

        public void AddVectors(IList<MoleculeAtomOrbitalPopulationVector> vectorsToAdd)
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
                foreach (MoleculeAtomOrbitalPopulationVector v in Vectors)
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
