namespace Molecules.Core.Domain.ValueObjects.Calc.Order.Info
{
    public record CreateInfoCalcOrder(string Name, string Description) : CalcOrderDetails(Name, Description);
}
