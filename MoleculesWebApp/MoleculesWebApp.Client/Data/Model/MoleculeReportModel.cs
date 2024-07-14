using System.Text;

namespace MoleculesWebApp.Client.Data.Model
{
    public class MoleculeReportModel
    {
        public MoleculeReportModel(int moleculeId)
        {
            MoleculeId = moleculeId;
            AtomPositions = new List<MoleculeAtomPositionReportItemModel>();
            ReportItems = new List<GeneralMoleculeReportItemModel>();
            MoleculeBonds = new List<MoleculeBondsReportItemModel>();
        }

        public string MoleculeName => ReportItems.Select(item => item.MoleculeName).FirstOrDefault("");

        public int MoleculeId { get; set; }


        public List<MoleculeAtomPositionReportItemModel> AtomPositions { get; set; }


        public List<GeneralMoleculeReportItemModel> ReportItems { get; set; }


        public List<MoleculeBondsReportItemModel> MoleculeBonds { get; set; }


        public string GetXyz()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine($"{AtomPositions.Count}");
            builder.AppendLine(MoleculeName);

            foreach (var atom in AtomPositions)
            {
                builder.AppendLine($"{atom.AtomSymbol} {atom.PosX:0.0000} {atom.PosY:0.0000} {atom.PosZ:0.0000}");
            }

            return builder.ToString();
        }
    }
}
