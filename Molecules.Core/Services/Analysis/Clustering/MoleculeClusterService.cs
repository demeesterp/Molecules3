using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Base;

namespace Molecules.Core.Services.Analysis.Clustering
{
    public class MoleculeClusterService : IMoleculeClusterService
    {
        private static List<MoleculesVector> GetRandomVectors(MoleculesVectorCollection moleculesVectorCollection, int nbrOfVectors)
        {
            int numberOfVectors = moleculesVectorCollection.Count;
            Random random = new Random();
            List<MoleculesVector> centroids = new List<MoleculesVector>();
            for (int i = 0; i < nbrOfVectors; i++)
            {
                centroids.Add(moleculesVectorCollection.At(random.Next(numberOfVectors)));
            }
            return centroids;
        }

        private static bool TryReassignLabels(MoleculesVectorCollection moleculesVectorCollection, int[] labels, List<MoleculesVector> centroids)
        {
            bool result = false;
            for (int i = 0; i < moleculesVectorCollection.Count; i++)
            {
                int newLabel = GetNearestCentroid(moleculesVectorCollection, i, centroids);
                if (newLabel != labels[i])
                {
                    labels[i] = newLabel;
                    result = true;
                }
            }
            return result;
        }

        private static int GetNearestCentroid(MoleculesVectorCollection moleculesVectorCollection, int vectorPos, List<MoleculesVector> centroids)
        {
            int nearest = 0;
            double minDistance = moleculesVectorCollection.At(vectorPos).GetDistance(centroids.First());
            for (int i = 1; i < centroids.Count; i++)
            {
                double distance = moleculesVectorCollection.At(vectorPos).GetDistance(centroids.ElementAt(i));
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearest = i;
                }
            }
            return nearest;
        }

        public List<MoleculesCluster<ClusterType>> KMeansCluster<ClusterType>(MoleculesVectorCollection moleculesVectorCollection, int numberOfClusters)
        {
            List<MoleculesVector> centroids = GetRandomVectors(moleculesVectorCollection, numberOfClusters);
            int vectorDimension = moleculesVectorCollection.Dimensions;
            bool changed = true;
            int iterations = 0;
            int[] labels = new int[moleculesVectorCollection.Count];
            if (labels.Length <= numberOfClusters)
            {
                return new List<MoleculesCluster<ClusterType>>();
            }
            while (changed && iterations < int.MaxValue)
            {
                iterations++;
                changed = TryReassignLabels(moleculesVectorCollection, labels, centroids);
                for (int centroidIndex = 0; centroidIndex < numberOfClusters; centroidIndex++)
                {
                    List<MoleculesVector> clusterPoints = new List<MoleculesVector>();
                    for (int vectorIndex = 0; vectorIndex < moleculesVectorCollection.Count; vectorIndex++)
                    {
                        if (labels[vectorIndex] == centroidIndex)
                        {
                            clusterPoints.Add(moleculesVectorCollection.At(vectorIndex));
                        }
                    }

                    if (clusterPoints.Count == 0)
                    {
                        continue; // Skip empty clusters
                    }

                    MoleculesVector newcentroid = moleculesVectorCollection.CreateEmptyCentroid();
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

            List<MoleculesCluster<ClusterType>> result = new List<MoleculesCluster<ClusterType>>();
            for (int i = 0; i < numberOfClusters; i++)
            {
                result.Add(new MoleculesCluster<ClusterType>(i));
            }
            for (int i = 0; i < labels.Length; ++i)
            {
                MoleculesCluster<ClusterType> cl = result.First(x => x.Label == labels[i]);

                cl.Add(moleculesVectorCollection.At(i));
            }
            return result;
        }
    }
}
