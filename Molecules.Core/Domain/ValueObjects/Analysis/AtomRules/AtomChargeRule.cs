
using Molecules.Core.Domain.ValueObjects.Analysis.RulesBase;

namespace Molecules.Core.Domain.ValueObjects.Analysis.AtomRules
{
    public class AtomPopulationRule : MoleculesRule<AtomRuleTag, AtomPopulationRuleVector>
    {
        public AtomPopulationRule(AtomRuleTag ruleTag, AtomPopulationRuleVector vector) 
            : base(ruleTag, vector) { }

    }
}
