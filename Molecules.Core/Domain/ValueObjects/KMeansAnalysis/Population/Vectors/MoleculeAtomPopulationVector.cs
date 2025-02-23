using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Base;

namespace Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Population.Vectors
{
    public class MoleculeAtomPopulationVector : MoleculesVector
    {

        public MoleculeAtomPopulationValues Values { get; set; } = new MoleculeAtomPopulationValues();

        public MoleculeAtomPopulationInfo Info { get; set; } = new MoleculeAtomPopulationInfo();


        public MoleculeAtomPopulationVector(string name)
            : base(name, 3) { }

        public override void AddToValue(int dimension, double valueToAdd)
        {
            switch (dimension)
            {
                case 0:
                    {
                        Values.MullikenPopulation += valueToAdd;
                        break;
                    }
                case 1:
                    {
                        Values.LowdinPopulation += valueToAdd;
                        break;
                    }
                case 2:
                    {
                        Values.AtomNumber += valueToAdd;
                        break;
                    }

                default:
                    {
                        throw ThrowInvalidDimensionError(dimension);
                    }
            }

        }

        public override double GetDistance(MoleculesVector vector)
        {
            return CalculateEuclidianDistance(vector);
        }

        public override double GetValue(int dimension)
        {
            switch (dimension)
            {
                case 0:
                    {
                        return Values.MullikenPopulation;
                    }
                case 1:
                    {
                        return Values.LowdinPopulation;
                    }
                case 2:
                    {
                        return Values.AtomNumber;
                    }
                default:
                    {

                        throw ThrowInvalidDimensionError(dimension);
                    }
            }
        }

        public override void MultiplyValueWith(int dimension, double multiplier)
        {
            switch (dimension)
            {
                case 0:
                    {
                        Values.MullikenPopulation *= multiplier;
                        break;
                    }
                case 1:
                    {
                        Values.LowdinPopulation *= multiplier;
                        break;
                    }
                case 2:
                    {
                        Values.AtomNumber *= multiplier;
                        break;
                    }

                default:
                    {

                        throw ThrowInvalidDimensionError(dimension);
                    }
            }
        }

        public override void SetValue(int dimension, double value)
        {
            switch (dimension)
            {
                case 0:
                    {
                        Values.MullikenPopulation = value;
                        break;
                    }
                case 1:
                    {
                        Values.LowdinPopulation = value;
                        break;
                    }
                case 2:
                    {
                        Values.AtomNumber = value;
                        break;
                    }
                default:
                    {

                        throw ThrowInvalidDimensionError(dimension);
                    }
            }
        }
    }
}
