namespace Molecules.Core.Domain.ValueObjects.Reports
{
    public class MoleculeAtomPositionReport
    {
        public string MoleculeName { get; set; } = "";
        public int AtomPosition { get; set; }
        public string AtomSymbol { get; set; } = "";
        public decimal? PosX { get; set; }
        public decimal? PosY { get; set; }
        public decimal? PosZ { get; set; }
    }
}
