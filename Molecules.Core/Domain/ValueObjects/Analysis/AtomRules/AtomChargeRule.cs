
using Molecules.Core.Domain.ValueObjects.Analysis.RulesBase;

namespace Molecules.Core.Domain.ValueObjects.Analysis.AtomRules
{
    public class AtomChargeRule : MoleculesRule<AtomRuleTag,AtomChargeRuleVector>
    {
        public AtomChargeRule(AtomRuleTag ruleTag, AtomChargeRuleVector vector) 
            : base(ruleTag, vector) { }

    }
}
