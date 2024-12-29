using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Base;

namespace Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Orbital.Vectors
{
    public class MoleculeAtomOrbitalPopulationVector : MoleculesVector
    {
        public MoleculeAtomOrbitalPopulationInfo Info = new MoleculeAtomOrbitalPopulationInfo();

        public MoleculeAtomOrbitalPopulationValues Values = new MoleculeAtomOrbitalPopulationValues();

        public MoleculeAtomOrbitalPopulationVector(string name) : base(name) { }

        public MoleculeAtomOrbitalPopulationVector(string name, int dimensions) : this(name) 
        {
            for(int dim= 0; dim < dimensions; ++dim)
            {
                var newItem = new MoleculeAtomOrbitalPopulationValueItem();
                Values.AppendItem(newItem);
                Info.Items.Add(newItem);
            }
        }

        public override int Dimensions 
        { 
            get => Values.Dimensions;
        }

        public override void AddToValue(int dimension, double valueToAdd) => Values[dimension] += valueToAdd;

        public override double GetDistance(MoleculesVector vector) => CalculateEuclidianDistance(vector);

        public override double GetValue(int dimension) => Values[dimension];

        public override void MultiplyValueWith(int dimension, double multiplier) => Values[dimension] *= multiplier;

        public override void SetValue(int dimension, double value) => Values[dimension] = value;
    }
}
