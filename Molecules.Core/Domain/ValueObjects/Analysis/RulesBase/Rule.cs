using System.Net.Http.Headers;

namespace Molecules.Core.Domain.ValueObjects.Analysis.RulesBased
{
    public class Rule<T, V> where T : IRuleTag where V : RuleVector
    {
        private T RuleTag { get; set; }
        private V Vector { get; set; }

        public Rule(T tag, V vector)
        {
            RuleTag = tag;
            Vector = vector;
        }

        public override string ToString()
        {
            return $"{Vector.Vector}->{RuleTag.Tag}";
        }


    }
}
