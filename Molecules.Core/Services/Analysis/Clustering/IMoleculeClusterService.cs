using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Base;

namespace Molecules.Core.Services.Analysis.Clustering
{
    public interface IMoleculeClusterService
    {
        List<MoleculesCluster<ClusterType>> KMeansCluster<ClusterType>(MoleculesVectorCollection moleculesVectorCollection, int numberOfClusters);
    }
}
