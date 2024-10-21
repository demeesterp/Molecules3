using Molecules.Core.Domain.ValueObjects.Analysis.RulesBase;
using Molecules.Core.Domain.ValueObjects.Molecules;
using Molecules.Shared;

namespace Molecules.Core.Domain.ValueObjects.Analysis.AtomRules
{
    public class AtomPopulationRuleVector : RuleVector
    {
        public readonly  string _mullikenCharge;

        public readonly string _lowdinCharge;

        public readonly string _atomNumber;
        
        public AtomPopulationRuleVector(Atom atom)
        {
            _mullikenCharge = atom.MullikenPopulation is null ? 
                                    Invalid : 
                                    $"{ StringConversion.ToString(atom.MullikenPopulation - atom.Number)}";
            _lowdinCharge = atom.LowdinPopulation is null ?
                                    Invalid :
                                    $"{StringConversion.ToString(atom.LowdinPopulation - atom.Number)}" ;

            _atomNumber = atom.Number.ToString();
        }

        protected override List<string> GetData()
        {
            return new List<string> { _mullikenCharge, _lowdinCharge, _atomNumber };
        }
    }
}
