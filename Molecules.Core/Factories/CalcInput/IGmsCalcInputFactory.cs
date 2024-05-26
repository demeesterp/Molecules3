using Molecules.Core.Domain.Aggregates;
using Molecules.Core.Domain.ValueObjects.GmsCalc;

namespace Molecules.Core.Factories.CalcInput
{
    public interface IGmsCalcInputFactory
    {
        List<GmsCalcInputItem> BuildCalcInput(IList<CalcOrder> orders);
    }
}
