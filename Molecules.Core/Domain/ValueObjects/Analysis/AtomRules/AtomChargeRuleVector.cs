using Molecules.Core.Domain.ValueObjects.Analysis.RulesBase;
using Molecules.Core.Domain.ValueObjects.Molecules;
using Molecules.Shared;

namespace Molecules.Core.Domain.ValueObjects.Analysis.AtomRules
{
    public class AtomChargeRuleVector : RuleVector
    {

        public readonly  string _mullikenCharge;

        public readonly string _lowdinCharge;
        
        public AtomChargeRuleVector(Atom atom)
        {
            _mullikenCharge = atom.MullikenPopulation is null ? 
                Invalid : 
                $"{ StringConversion.ToString(atom.MullikenPopulation - atom.Number)}";
            _lowdinCharge = atom.LowdinPopulation is null ?
                Invalid :
                $"{StringConversion.ToString(atom.LowdinPopulation - atom.Number)}" ;
        }

        protected override List<string> GetData()
        {
            return new List<string> { _mullikenCharge, _lowdinCharge };
        }
    }
}
