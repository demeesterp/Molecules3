using Molecules.Core.Domain.Entities;
using Molecules.Core.Domain.ValueObjects.Calc.Order;

namespace Molecules.Core.Domain.Aggregates
{
    public class CalcOrder(int id, CalcOrderDetails details, CalcOrderItem[] items)
    {
        public int Id { get; } = id;

        public CalcOrderDetails Details { get; } = details;

        public CalcOrderItem[] Items { get; } = items;

    }
}
