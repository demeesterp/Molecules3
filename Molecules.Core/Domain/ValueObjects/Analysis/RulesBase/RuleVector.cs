namespace Molecules.Core.Domain.ValueObjects.Analysis.RulesBase
{
    public abstract class RuleVector
    {
        protected const string Invalid = nameof(Invalid);

        protected abstract List<string> GetData();

        public string Vector()
        {
            return $"{{{string.Join(',', GetData())}}}";
        }
    }
}
