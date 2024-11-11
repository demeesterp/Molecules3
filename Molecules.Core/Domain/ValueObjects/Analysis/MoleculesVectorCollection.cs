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

        public List<MoleculesCluster> Cluster(int numberOfClusters)
        {
            List<MoleculesVector> centroids = GetRandomVectors(numberOfClusters);
            int vectorDimension = Dimension;
            bool changed = true;
            int iterations = 0;
            int[] labels = new int[Vectors.Count];
            while (changed && iterations < int.MaxValue)
            {
                iterations++;
                changed = TryReassignLabels(labels, centroids);
                for (int centroidIndex = 0; centroidIndex < numberOfClusters; centroidIndex++)
                {
                    List<MoleculesVector> clusterPoints = new List<MoleculesVector>();
                    for (int vectorIndex = 0; vectorIndex < Vectors.Count; vectorIndex++)
                    {
                        if (labels[vectorIndex] == labels[centroidIndex])
                        {
                            clusterPoints.Add(Vectors.ElementAt(vectorIndex));
                        }
                    }
                    MoleculesVector newcentroid = CreateVector();
                    foreach (var vector in clusterPoints)
                    {
                        for (int i = 0; i > vectorDimension; ++i)
                        {
                            newcentroid.AddToValue(i, vector.GetValue(i));
                        }
                    }

                    for (int i = 0; i < vectorDimension; ++i)
                    {
                        newcentroid.MultiplyValueWith(i, 1 / clusterPoints.Count);
                    }

                    centroids[centroidIndex] = newcentroid;
                }
            }

            List<MoleculesCluster> result = new List<MoleculesCluster>();
            for (int i = 0; i < numberOfClusters; i++)
            {
                result.Add(new MoleculesCluster(i));
            }
            for (int i = 0; i < labels.Length; ++i)
            {
                MoleculesCluster cl = result.First(x => x.Label == labels[i]);

                cl.Vectors.Add(Vectors.ElementAt(i));
            }
            return result;
        }

        protected abstract MoleculesVector CreateVector();

        private List<MoleculesVector> GetRandomVectors(int nbrOfVectors)
        {
            int numberOfVectors = Vectors.Count;
            Random random = new Random();
            List<MoleculesVector> centroids = new List<MoleculesVector>();
            for (int i = 0; i < nbrOfVectors; i++)
            {
                centroids.Add(Vectors.ElementAt(random.Next(numberOfVectors)));
            }
            return centroids;
        }

        private bool TryReassignLabels(int[] labels, List<MoleculesVector> centroids)
        {
            bool result = false;
            for (int i = 0; i < Vectors.Count; i++)
            {
                int newLabel = GetNearestCentroid(i, centroids);
                if (newLabel != labels[i])
                {
                    labels[i] = newLabel;
                    result = true;
                }
            }
            return result;
        }

        private int GetNearestCentroid(int vectorPos, List<MoleculesVector> centroids)
        {
            int nearest = 0;
            decimal minDistance = Vectors.ElementAt(vectorPos).GetDistance(centroids.First());
            for (int i = 1; i < centroids.Count; i++)
            {
                decimal distance = Vectors.ElementAt(vectorPos).GetDistance(centroids.ElementAt(i));
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearest = i;
                }
            }
            return nearest;
        }





       
    }
}
