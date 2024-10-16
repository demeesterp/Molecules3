using System.Net.Http.Headers;

namespace Molecules.Core.Domain.ValueObjects.Analysis.RulesBase
{
    public class MoleculesRule<T, V> where T : IRuleTag where V : RuleVector
    {
        private T RuleTag { get; set; }
        private V Vector { get; set; }

        public MoleculesRule(T tag, V vector)
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
