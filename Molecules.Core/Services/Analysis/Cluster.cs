using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Molecules.Core.Services.Analysis
{
    public class KMeans
    {


        public static void Test()
        {
            double[][] data = new double[][]
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

            int numberOfClusters = 3;
            List<int> labels = KMeans.Cluster(data, numberOfClusters);

            for (int i = 0; i < data.Length; i++)
            {
                Console.WriteLine($"Point {i}: ({string.Join(", ", data[i])}) - Cluster {labels[i]}");
            }
        }



        public static List<int> Cluster(double[][] data, int numberOfClusters, int maxIterations = 100)
        {
            int numberOfVectors = data.Length;
            int vectorDimensions = data[0].Length;
            Random random = new Random();
            double[][] centroids = new double[numberOfClusters][];
            for (int i = 0; i < numberOfClusters; i++)
            {
                centroids[i] = data[random.Next(numberOfVectors)];
            }

            int[] labels = new int[numberOfVectors];
            bool changed = true;
            int iterations = 0;

            while (changed && iterations < maxIterations)
            {
                changed = false;
                iterations++;

                // Assign labels
                for (int i = 0; i < numberOfVectors; i++)
                {
                    int newLabel = GetNearestCentroid(data[i], centroids);
                    if (newLabel != labels[i])
                    {
                        labels[i] = newLabel;
                        changed = true;
                    }
                }

                // Update centroids
                for (int i = 0; i < numberOfClusters; i++)
                {
                    double[] newCentroid = new double[vectorDimensions];
                    int count = 0;
                    for (int j = 0; j < numberOfVectors; j++)
                    {
                        if (labels[j] == i)
                        {
                            for (int l = 0; l < vectorDimensions; l++)
                            {
                                newCentroid[l] += data[j][l];
                            }
                            count++;
                        }
                    }
                    if (count > 0)
                    {
                        for (int l = 0; l < vectorDimensions; l++)
                        {
                            newCentroid[l] /= count;
                        }
                        centroids[i] = newCentroid;
                    }
                }
            }

            return labels.ToList();
        }

        private static int GetNearestCentroid(double[] point, double[][] centroids)
        {
            int nearest = 0;
            double minDistance = Distance(point, centroids[0]);
            for (int i = 1; i < centroids.Length; i++)
            {
                double distance = Distance(point, centroids[i]);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearest = i;
                }
            }
            return nearest;
        }

        private static double Distance(double[] point1, double[] point2)
        {
            double sum = 0;
            for (int i = 0; i < point1.Length; i++)
            {
                sum += Math.Pow(point1[i] - point2[i], 2);
            }
            return Math.Sqrt(sum);
        }
    }
}
