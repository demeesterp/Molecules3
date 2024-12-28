using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Base;

namespace Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Orbital
{
    public class MoleculesAtomOrbitalPopulationVector : MoleculesVector
    {
        public MoleculesAtomOrbitalPopulationInfo Info = new MoleculesAtomOrbitalPopulationInfo();

        public MoleculesAtomOrbitalPopulationValues Values = new MoleculesAtomOrbitalPopulationValues();

        public MoleculesAtomOrbitalPopulationVector(string name):base(name) { }

        public override void AddToValue(int dimension, double valueToAdd) => Values[dimension] += valueToAdd;

        public override double GetDistance(MoleculesVector vector) => CalculateEuclidianDistance(vector);

        public override double GetValue(int dimension) => Values[dimension];

        public override void MultiplyValueWith(int dimension, double multiplier) => Values[dimension] *= multiplier;

        public override void SetValue(int dimension, double value) => Values[dimension] = value;
    }
}
