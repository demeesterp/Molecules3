using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Base;
using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Orbital.Vectors;

namespace Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Orbital.VectorCollections
{
    public class MoleculesAtomOrbitalPopulationVectorCollection : MoleculesVectorCollection
    {
        public override MoleculesVector CreateEmptyCentroid()
        {
            return new MoleculesAtomOrbitalPopulationVector(string.Empty);
        }

        public int AtomNumber
        {
            get;
            set;
        }

        public MoleculesAtomOrbitalPopulationVectorCollection(double atomNumber)
        {
            AtomNumber = (int)Math.Round(atomNumber);
        }

        public MoleculesAtomOrbitalPopulationVector AddVector(MoleculesAtomOrbitalPopulationVector toAdd)
        {
            return (MoleculesAtomOrbitalPopulationVector)base.AddVector(toAdd);
        }

        public void AddVectors(IList<MoleculesAtomOrbitalPopulationVector> vectorsToAdd)
        {
            AddVectors(vectorsToAdd.Cast<MoleculesVector>().ToList());
        }

        public override void Normalize()
        {

        }
    }
}
