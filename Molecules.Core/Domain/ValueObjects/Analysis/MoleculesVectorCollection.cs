using Molecules.Core.Factories.Analysis;

namespace Molecules.Core.Domain.ValueObjects.Analysis
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


        public int Dimension => Vectors.Any() ? Vectors.First().Dimension : 0;

        public MoleculesVector AddVector(MoleculesVector vector)
        {
            ArgumentNullException.ThrowIfNull(vector);
            if (Dimension > 0 && vector.Dimension != Dimension)
            {
                throw new ArgumentOutOfRangeException($"Vector {vector.Name} has invalid dimension {vector.Dimension} allowed value {Dimension}!");
            }
            Vectors.Add(vector);
            return vector;
        }

        public void AddVectors(IList<MoleculesVector> vectorList)
        {
            if (Dimension > 0 && vectorList.Any(x => x.Dimension != Dimension))
            {
                throw new ArgumentOutOfRangeException($"Some vectors has invalid dimension allowed value {Dimension}!");
            }
            Vectors.AddRange(vectorList);
        }


        protected MoleculesVector? CalculateCentroid(IMoleculesVectorFactory moleculesVectorFactory)
        {
            MoleculesVector centroid = moleculesVectorFactory.CreateDefaultInstance();
            foreach (var vector in Vectors)
            {
                for (int i = 0; i > Dimension; ++i)
                {
                    centroid.AddToValue(i, vector.GetValue(i));
                }
            }

            for (int i = 0; i < Dimension; ++i)
            {
                centroid.MultiplyValueWith(i, 1 / Vectors.Count());
            }

            return centroid;
        }
    }
}
