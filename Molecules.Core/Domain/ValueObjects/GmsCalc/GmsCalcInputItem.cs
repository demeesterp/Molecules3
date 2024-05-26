namespace Molecules.Core.Domain.ValueObjects.GmsCalc
{
    public class GmsCalcInputItem(string orderName,
                                    int orderItemId,
                                    string moleculeName,
                                    GmsCalculationKind kind,
                                    string content)
    {
        private readonly int _orderItemId = orderItemId;

        private readonly string _orderName = orderName;

        public string MoleculeName { get; } = moleculeName;

        public GmsCalculationKind Kind { get; } = kind;

        public string Content { get; } = content;

        public string DisplayName => $"{_orderName}_{_orderItemId}_{MoleculeName}_{Kind}";

    }
}
