using MoleculesWebApp.Client.Data.ServiceAgents.Molecules;

namespace MoleculesWebApp.Client.Data.Model.Molecule
{
    public class MoleculesOverviewItemModel
    {
        public MoleculesOverviewItemModel(CalcMolecule calcMolecule)
        {
            Id = calcMolecule.Id;
            Name = calcMolecule.MoleculeName;
            CalcOrderName = calcMolecule.OrderName;
            BasisSetName = calcMolecule.BasisSet;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string CalcOrderName { get; set; }

        public string BasisSetName { get; set; }

        public CalcMolecule GetCalcMolecule()
        {
            return new CalcMolecule()
            {
                Id = Id,
                MoleculeName = Name,
                OrderName = CalcOrderName,
                BasisSet = BasisSetName,
            };
        }
    }
}
