﻿namespace Molecules.Core.Domain.ValueObjects.Analysis
{
    public abstract class MoleculesVector
    {

        protected Exception ThrowInvalidDimensionError(int usedDimension)
        {
            return new ArgumentOutOfRangeException($"Dimension {usedDimension} is higher then dimensions of the vector {Dimensions}");
        }


        protected double CalculateEuclidianDistance(MoleculesVector moleculesVector)
        {
            double sum = 0;
            for (int dimension = 0; dimension < Dimensions; dimension++)
            {
               sum += Math.Pow(GetValue(dimension) - moleculesVector.GetValue(dimension),2);
            }
            return Math.Sqrt(sum);
        }

        protected MoleculesVector(string name, int dimensions)
        {
            Name = name;
            Dimensions = dimensions;
        }

        public int Dimensions
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public abstract double GetDistance(MoleculesVector vector);

        public abstract double GetValue(int dimension);

        public abstract void SetValue(int dimension, double value);

        public abstract void AddToValue(int dimension, double valueToAdd);

        public abstract void MultiplyValueWith(int dimension, double multiplier);


    }
}