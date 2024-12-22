namespace Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Base;


public class MoleculesCluster
{
    public MoleculesCluster(int label)
    {
        Label = label;
        Vectors = new List<MoleculesVector>();
    }

    public int Label { get; set; }

    public List<MoleculesVector> Vectors { get; set; }
}