namespace Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Base
{
    public abstract class MoleculesVectorCollection
    {
        protected List<MoleculesVector> Vectors
        {
            get;
            set;
        }

        protected MoleculesVectorCollection()
        {
            Vectors = new List<MoleculesVector>();
        }

        protected MoleculesVectorCollection(IList<MoleculesVector> vectorList) : this()
        {
            AddVectors(vectorList);
        }

        public int Dimensions => Vectors.Any() ? Vectors.First().Dimensions : 0;

        public int Count => Vectors.Count;

        public abstract MoleculesVector CreateEmptyCentroid();

        public abstract void Normalize();

        public MoleculesVector At(int position) => Vectors.ElementAt(position);

        protected MoleculesVector AddVector(MoleculesVector vector)
        {
            ArgumentNullException.ThrowIfNull(vector);
            if (Dimensions > 0 && vector.Dimensions != Dimensions)
            {
                throw new ArgumentOutOfRangeException($"Vector {vector.Name} has invalid dimension {vector.Dimensions} allowed value {Dimensions}!");
            }
            Vectors.Add(vector);
            return vector;
        }

        protected void AddVectors(IList<MoleculesVector> vectorList)
        {
            if (Dimensions > 0 && vectorList.Any(x => x.Dimensions != Dimensions))
            {
                throw new ArgumentOutOfRangeException($"Some vectors has invalid dimension allowed value {Dimensions}!");
            }
            Vectors.AddRange(vectorList);
        }


    }
}
