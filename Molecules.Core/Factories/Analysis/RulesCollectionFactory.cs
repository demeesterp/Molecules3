using Molecules.Core.Domain.ValueObjects.Analysis.AtomRules;
using Molecules.Core.Domain.ValueObjects.Molecules;

namespace Molecules.Core.Factories.Analysis
{
    public class RulesCollectionFactory : IRulesCollectionFactory
    {
        public AtomChargeRuleCollection BuildAtomChargeRuleCollection(List<Molecule> molecules)
        {
            AtomChargeRuleCollection result = new AtomChargeRuleCollection();
            foreach (Molecule molecule in molecules)
            {
                foreach (Atom atom in molecule.Atoms)
                {
                    result.Add(new AtomChargeRule(new AtomRuleTag(atom, molecule.Name),
                                    new AtomChargeRuleVector(atom)));
                }
            }
            return result;
        }
    }
}
