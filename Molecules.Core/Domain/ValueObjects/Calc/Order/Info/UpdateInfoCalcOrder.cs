namespace Molecules.Core.Domain.ValueObjects.Calc.Order.Info
{
    public record UpdateInfoCalcOrder(string Name, string Description) : CalcOrderDetails(Name, Description);
}
