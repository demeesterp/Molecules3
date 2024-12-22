using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Base;

namespace Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Population
{
    public class MoleculeAtomPopulationVector : MoleculesVector
    {
        public double MullikenPopulation { get; set; }

        public double LowdinPopulation { get; set; }

        public double AtomNumber { get; set; }


        public MoleculeAtomPopulationVector(string name) : base(name, 3)
        {
        }

        public override void AddToValue(int dimension, double valueToAdd)
        {
            switch (dimension)
            {
                case 0:
                    {
                        MullikenPopulation += valueToAdd;
                        break;
                    }
                case 1:
                    {
                        LowdinPopulation += valueToAdd;
                        break;
                    }
                case 2:
                    {
                        AtomNumber += valueToAdd;
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
                        return MullikenPopulation;
                    }
                case 1:
                    {
                        return LowdinPopulation;
                    }
                case 2:
                    {
                        return AtomNumber;
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
                        MullikenPopulation *= multiplier;
                        break;
                    }
                case 1:
                    {
                        LowdinPopulation *= multiplier;
                        break;
                    }
                case 2:
                    {
                        AtomNumber *= multiplier;
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
                        MullikenPopulation = value;
                        break;
                    }
                case 1:
                    {
                        LowdinPopulation = value;
                        break;
                    }
                case 2:
                    {
                        AtomNumber = value;
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
