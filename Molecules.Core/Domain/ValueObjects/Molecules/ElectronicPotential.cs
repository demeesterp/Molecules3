namespace Molecules.Core.Domain.ValueObjects.Molecules
{
    public class ElectronicPotential
    {
        public string? Type { get; set; }
        public double? PosX { get; set; }
        public double? PosY { get; set; }
        public double? PosZ { get; set; }
        public double? Nuclear { get; set; }
        public double? Electronic { get; set; }
        public double? Total { get; set; }
    }
}
