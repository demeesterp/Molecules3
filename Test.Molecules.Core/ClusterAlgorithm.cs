using System.Text;

namespace Test.Molecules.Core
{
    public class ClusterAlgorithm
    {

        public static string Test(int numberOfClusters, int maxIterations)
        {

            StringBuilder result = new StringBuilder();

            List<double[]> data = new List<double[]>
            {
                new double[] { 1.0, 2.0 },
                new double[] { 1.5, 1.8 },
                new double[] { 5.0, 8.0 },
                new double[] { 8.0, 8.0 },
                new double[] { 1.0, 0.6 },
                new double[] { 9.0, 11.0 },
                new double[] { 8.0, 2.0 },
                new double[] { 10.0, 2.0 },
                new double[] { 9.0, 3.0 }
            };

            List<int> labels = Cluster(data, numberOfClusters, EuclideanDistance, maxIterations);

            for (int i = 0; i < data.Count; i++)
            {
                result.AppendLine($"Point {i}: ({string.Join(", ", data[i])}) - Cluster {labels[i]}");
            }

            return result.ToString();

        }

        public static double EuclideanDistance(double[] point1, double[] point2)
        {
            double sum = 0;
            for (int i = 0; i < point1.Length; i++)
            {
                sum += Math.Pow(point1[i] - point2[i], 2);
            }
            return Math.Sqrt(sum);
        }



        public delegate double DistanceFunction<T>(T point1, T point2);


        public static List<int> Cluster<T>(List<T> inputData, int numberOfClusters, DistanceFunction<T> distanceFunc, int maxIterations = 100) where T : IEnumerable<double>
        {
            int numberOfVectors = inputData.Count;
            Random random = new Random();
            List<T> centroids = new List<T>();

            for (int i = 0; i < numberOfClusters; i++)
            {
                centroids.Add(inputData[random.Next(numberOfVectors)]);
            }

            int[] labels = new int[numberOfVectors];
            bool changed = true;
            int iterations = 0;

            while (changed && iterations < maxIterations)
            {
                changed = false;
                iterations++;

                // Assign labels indicating to what cluster  vector belongs too
                for (int i = 0; i < numberOfVectors; i++)
                {
                    int newLabel = GetNearestCentroid(inputData[i], centroids, distanceFunc);
                    if (newLabel != labels[i])
                    {
                        labels[i] = newLabel;
                        changed = true;
                    }
                }

                // With the labels assigned create a vector collection and transfor it into a centroid
                // ( a vector that designates the central point of a cluster )
                for (int i = 0; i < numberOfClusters; i++)
                {
                    List<T> clusterPoints = new List<T>();
                    for (int j = 0; j < numberOfVectors; j++)
                    {
                        if (labels[j] == i)
                        {
                            clusterPoints.Add(inputData[j]);
                        }
                    }
                    if (clusterPoints.Count > 0)
                    {
                        centroids[i] = CalculateCentroid(clusterPoints);
                    }
                }
            }

            return labels.ToList();
        }

        private static int GetNearestCentroid<T>(T point, List<T> centroids, DistanceFunction<T> distanceFunc)
        {
            int nearest = 0;
            double minDistance = distanceFunc(point, centroids[0]);
            for (int i = 1; i < centroids.Count; i++)
            {
                double distance = distanceFunc(point, centroids[i]);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearest = i;
                }
            }
            return nearest;
        }

        private static T CalculateCentroid<T>(List<T> clusterPoints) where T : IEnumerable<double>
        {
            int dimensions = clusterPoints[0].Count();
            double[] centroid = new double[dimensions];
            foreach (var point in clusterPoints)
            {
                int i = 0;
                foreach (var value in point)
                {
                    centroid[i++] += value;
                }
            }
            for (int i = 0; i < dimensions; i++)
            {
                centroid[i] /= clusterPoints.Count;
            }
            return (T)(IEnumerable<double>)centroid;
        }


        public static double[] CalculateCentroid(List<double[]> clusterPoints)
        {
            int dimensions = clusterPoints[0].Length;
            double[] centroid = new double[dimensions];
            foreach (var point in clusterPoints)
            {
                for (int i = 0; i < dimensions; i++)
                {
                    centroid[i] += point[i];
                }
            }
            for (int i = 0; i < dimensions; i++)
            {
                centroid[i] /= clusterPoints.Count;
            }
            return centroid;
        }





    }
}
