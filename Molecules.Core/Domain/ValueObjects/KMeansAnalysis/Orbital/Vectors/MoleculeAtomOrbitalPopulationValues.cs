namespace Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Orbital.Vectors
{
    public class MoleculeAtomOrbitalPopulationValues
    {
        private List<double> values = new();

        public int Dimensions => values.Count;

        public int AtomNumber { get; set; }

        public void AppendItem(MoleculeAtomOrbitalPopulationValueItem item)
        {
            values.Add(item.LowdinPopulation);
            values.Add(item.MullikenPopulation);
        }


        public double this[int index]
        {
            get => values[index];
            set => values[index] = value;
        }

    }
}
