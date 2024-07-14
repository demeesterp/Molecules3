namespace MoleculesWebApp.Client.Data.ServiceAgents.Molecules.Report
{
    public class MoleculeAtomsChargeReport
    {
        public string MoleculeName { get; set; } = "";

        public string AtomID { get; set; } = "";

        public decimal? MullikenCharge { get; set; }

        public decimal? LowdinCharge { get; set; }

        public decimal? CHelpGHCharge { get; set; }

        public decimal? GeoDiscCharge { get; set; }
    }
}
