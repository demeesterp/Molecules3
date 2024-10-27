using Molecules.Core.Domain.ValueObjects.Analysis.RulesBase;
using Molecules.Core.Domain.ValueObjects.Molecules;

namespace Molecules.Core.Domain.ValueObjects.Analysis.AtomRules
{
    public class AtomRuleTag : IRuleTag
    {
        private readonly string  _tag;

        public AtomRuleTag(Atom atom, string moleculeName )
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(moleculeName);
            _tag = $"{atom.Symbol}{atom.Position}_{moleculeName}";
        }

        public string Tag() => _tag;
    }
}
