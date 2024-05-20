namespace Molecules.Core.Data.DbEntities
{
    internal class CalcOrderDbEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";

        public string Description { get; set; } = "";

        public string CustomerName { get; set; } = "";

        public ICollection<CalcOrderItemDbEntity> CalcOrderItems { get; set; } = new List<CalcOrderItemDbEntity>();
    }
}
