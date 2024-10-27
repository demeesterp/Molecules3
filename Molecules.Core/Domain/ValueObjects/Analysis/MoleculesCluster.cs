namespace Molecules.Core.Domain.ValueObjects.Analysis
{
    public class MoleculesCluster : MoleculesVectorCollection
    {
        public MoleculesCluster(int label) : base()
        {
            Label = label;
        }

        public int Label
        {
            get;
            set;
        }

    }
}
