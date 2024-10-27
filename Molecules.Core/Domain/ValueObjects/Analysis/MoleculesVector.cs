namespace Molecules.Core.Domain.ValueObjects.Analysis
{
    public abstract class MoleculesVector
    {
        protected MoleculesVector(string name, int dimension)
        {
            Name = name;
            Dimension = dimension;
        }

        public int Dimension
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public abstract decimal GetDistance(MoleculesVector vector);

        public abstract decimal GetValue(int dimension);

        public abstract void SetValue(int dimension, decimal value);

        public abstract void AddToValue(int dimension, decimal valueToAdd);

        public abstract void MultiplyValueWith(int dimention, decimal multiplier);


    }
}
