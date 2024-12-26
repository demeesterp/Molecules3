using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Base;

namespace Molecules.Core.Services.Analysis.Clustering
{
    public interface IMoleculeClusterService
    {
        public List<(double, string)> CalculateWCSSForRange<ClusterType>(MoleculesVectorCollection moleculesVectorCollection, int minClusters, int maxClusters);
        List<MoleculesCluster<ClusterType>> KMeansCluster<ClusterType>(MoleculesVectorCollection moleculesVectorCollection, int numberOfClusters);
    }
}
