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

        public int Dimensions => Vectors.Any() ? Vectors.First().Dimensions : 0;

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

        public List<MoleculesCluster> KMeansCluster(int numberOfClusters)
        {
            List<MoleculesVector> centroids = GetRandomVectors(numberOfClusters);
            int vectorDimension = Dimensions;
            bool changed = true;
            int iterations = 0;
            int[] labels = new int[Vectors.Count];

            Normalize();

            if ( labels.Length <= numberOfClusters)
            {
                return new List<MoleculesCluster>();
            }
            while (changed && iterations < int.MaxValue)
            {
                iterations++;
                changed = TryReassignLabels(labels, centroids);
                for (int centroidIndex = 0; centroidIndex < numberOfClusters; centroidIndex++)
                {
                    List<MoleculesVector> clusterPoints = new List<MoleculesVector>();
                    for (int vectorIndex = 0; vectorIndex < Vectors.Count; vectorIndex++)
                    {
                        if (labels[vectorIndex] == centroidIndex)
                        {
                            clusterPoints.Add(Vectors.ElementAt(vectorIndex));
                        }
                    }

                    if (clusterPoints.Count == 0)
                    {
                        continue; // Skip empty clusters
                    }

                    MoleculesVector newcentroid = CreateEmptyCentroid();
                    foreach (var vector in clusterPoints)
                    {
                        for (int i = 0; i < vectorDimension; ++i)
                        {
                            newcentroid.AddToValue(i, vector.GetValue(i));
                        }
                    }

                    for (int i = 0; i < vectorDimension; ++i)
                    {
                        newcentroid.MultiplyValueWith(i, 1.0 / clusterPoints.Count);
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

        protected abstract MoleculesVector CreateEmptyCentroid();

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
            double minDistance = Vectors.ElementAt(vectorPos).GetDistance(centroids.First());
            for (int i = 1; i < centroids.Count; i++)
            {
                double distance = Vectors.ElementAt(vectorPos).GetDistance(centroids.ElementAt(i));
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearest = i;
                }
            }
            return nearest;
        }


        protected abstract void Normalize();
       
    }
}
