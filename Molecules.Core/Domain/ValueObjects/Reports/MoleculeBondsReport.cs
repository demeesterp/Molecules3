namespace Molecules.Core.Domain.ValueObjects.Reports
{
    public class MoleculeBondsReport
    {
        public string MoleculeName { get; set; } = "";

        public int Atom1Pos { get; set; }

        public int Atom2Pos { get; set; }

        public string BondID { get; set; } = "";
        public decimal? Distance { get; set; }
        public decimal? BondOrder { get; set; }
        public decimal? OverlapPopulation { get; set; }
        public decimal? OverlapPopulationHOMO { get; set; }
        public decimal? OverlapPopulationLUMO { get; set; }
    }
}
