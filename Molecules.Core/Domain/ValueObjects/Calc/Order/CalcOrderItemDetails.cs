namespace Molecules.Core.Domain.ValueObjects.Calc.Order
{
    public  record CalcOrderItemDetails(int Charge, string Xyz, CalcBasisSetCode CalcBasisSetCode, CalcOrderItemType Type);
}
