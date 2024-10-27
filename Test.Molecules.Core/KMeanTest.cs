namespace Test.Molecules.Core
{
    public class KMeanTest
    {
        [Fact]
        public void Test()
        {
           var result =  ClusterAlgorithm.Test(4, int.MaxValue);



            // make a random list of centroids chosen from the input vector             


            // Assign for each vector to what centroid it is the nearest


            // For each centroid we have a list of vectors


           Assert.NotEmpty(result);
        }
    }
}
