using Molecules.Core.Domain.ValueObjects.Calc.Order;

namespace Molecules.Core.Domain.Entities
{
    public class CalcOrderItem(int id, string moleculeName, CalcOrderItemDetails details )
    {
        public int Id { get; } = id;

        public string MoleculeName { get; } = moleculeName;

        public CalcOrderItemDetails Details { get; } = details;

    }
}
