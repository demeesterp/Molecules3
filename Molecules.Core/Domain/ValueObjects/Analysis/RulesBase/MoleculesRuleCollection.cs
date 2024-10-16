using System.Text;

namespace Molecules.Core.Domain.ValueObjects.Analysis.RulesBase
{
    public class MoleculesRuleCollection<T, V> where T : IRuleTag where V : RuleVector
    {
        private List<MoleculesRule<T, V>> Rules { get; set; } = new List<MoleculesRule<T, V>>();

        public MoleculesRuleCollection() { }

        public MoleculesRuleCollection(IEnumerable<MoleculesRule<T, V>> rules)
        {
            Rules.AddRange(rules);
        }

        public void Add(MoleculesRule<T, V> rule)
        {
            Rules.Add(rule);
        }

        public void AddRange(IEnumerable<MoleculesRule<T, V>> rules)
        {
            Rules.AddRange(rules);
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            foreach (var item in Rules)
            {
                result.AppendLine(item.ToString());
            }
            return result.ToString();
        }

    }
}
