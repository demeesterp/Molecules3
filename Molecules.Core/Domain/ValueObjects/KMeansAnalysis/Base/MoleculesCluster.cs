using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Population;
using System.Collections;

namespace Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Base;


public class MoleculesCluster<ClusterType>(int label, List<MoleculesVector> moleculesVectors) : IEnumerable<ClusterType>
{
    protected List<MoleculesVector> Vectors { get; set; } = moleculesVectors;

    public MoleculesCluster(int label): this(label, new List<MoleculesVector>()) { }

    public MoleculesCluster(MoleculesCluster<ClusterType> toCopy): this(toCopy.Label, toCopy.Vectors) { }


    public int Label { get; set; } = label;


    public virtual MoleculesVector Add(MoleculesVector toAdd)
    {
        Vectors.Add(toAdd);
        return toAdd;
    }

    public IEnumerator<ClusterType> GetEnumerator()
    {
        return Vectors.Cast<ClusterType>().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return Vectors.GetEnumerator();
    }
}