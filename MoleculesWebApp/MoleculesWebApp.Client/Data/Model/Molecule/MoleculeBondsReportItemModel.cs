using MoleculesWebApp.Client.Data.ServiceAgents.Molecules.Report;

namespace MoleculesWebApp.Client.Data.Model.Molecule
{
    public class MoleculeBondsReportItemModel
    {
        public MoleculeBondsReportItemModel(MoleculeBondsReport toCopy)
        {
            MoleculeName = toCopy.MoleculeName;
            BondID = toCopy.BondID;
            Distance = toCopy.Distance;
            BondOrder = toCopy.BondOrder;
            OverlapPopulation = toCopy.OverlapPopulation;
            OverlapPopulationHOMO = toCopy.OverlapPopulationHOMO;
            OverlapPopulationLUMO = toCopy.OverlapPopulationLUMO;
            Atom1Pos = toCopy.Atom1Pos;
            Atom2Pos = toCopy.Atom2Pos;
        }

        public string MoleculeName { get; set; } = "";
        public string BondID { get; set; } = "";
        public int Atom1Pos { get; set; }
        public int Atom2Pos { get; set; }
        public decimal? Distance { get; set; }
        public decimal? BondOrder { get; set; }
        public decimal? OverlapPopulation { get; set; }
        public decimal? OverlapPopulationHOMO { get; set; }
        public decimal? OverlapPopulationLUMO { get; set; }

        //public BondOrder BondOrderEnum()
        //{
        //    if (BondOrder >= 0.6m && BondOrder < 1.6m)
        //    {
        //        return NCDK.BondOrder.Single;
        //    }
        //    else if (BondOrder >= 1.6m && BondOrder < 2.6m)
        //    {
        //        return NCDK.BondOrder.Double;
        //    }
        //    else if (BondOrder >= 2.6m && BondOrder < 3.6m)
        //    {
        //        return NCDK.BondOrder.Triple;
        //    }
        //    return NCDK.BondOrder.Unset;
        //}
    }
}
