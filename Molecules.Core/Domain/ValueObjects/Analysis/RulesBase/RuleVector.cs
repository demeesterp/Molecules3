namespace Molecules.Core.Domain.ValueObjects.Analysis.RulesBased
{
    public abstract class RuleVector
    {
        protected abstract List<string> GetData();

        public string Vector()
        {
            return $"{{{string.Join(',', GetData())}}}";
        }
    }
}
