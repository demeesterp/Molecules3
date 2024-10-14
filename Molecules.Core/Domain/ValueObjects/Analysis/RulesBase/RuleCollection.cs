

using System.Text;

namespace Molecules.Core.Domain.ValueObjects.Analysis.RulesBased
{
    public class RuleCollection<T, V> where T : IRuleTag where V : RuleVector
    {
        private List<Rule<T, V>> Rules { get; set; } = new List<Rule<T, V>>();

        public RuleCollection() { }

        public RuleCollection(IEnumerable<Rule<T, V>> rules)
        {
            Rules.AddRange(rules);
        }

        public void Add(Rule<T, V> rule)
        {
            Rules.Add(rule);
        }

        public void AddRange(IEnumerable<Rule<T, V>> rules)
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
