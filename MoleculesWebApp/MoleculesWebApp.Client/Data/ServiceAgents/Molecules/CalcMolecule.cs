namespace MoleculesWebApp.Client.Data.ServiceAgents.Molecules
{
    public class CalcMolecule
    {
        public int Id { get; set; }

        public string OrderName { get; set; }

        public string BasisSet { get; set; }

        public string MoleculeName { get; set; }

        public Molecule? Molecule { get; set; }

        public CalcMolecule()
        {
            OrderName = "";
            BasisSet = "";
            MoleculeName = "";
        }
    }
}
