using Molecules.Core.Domain.ValueObjects.Analysis.AtomRules;
using Molecules.Core.Domain.ValueObjects.Molecules;

namespace Molecules.Core.Factories.Analysis
{
    public class RulesCollectionFactory : IRulesCollectionFactory
    {
        public AtomPopulationRuleCollection BuildAtomChargeRuleCollection(List<Molecule> molecules)
        {
            AtomPopulationRuleCollection result = new AtomPopulationRuleCollection();
            foreach (Molecule molecule in molecules)
            {
                foreach (Atom atom in molecule.Atoms)
                {
                    var tag = new AtomRuleTag(atom, molecule.Name);
                    var vector = new AtomPopulationRuleVector(atom);
                    if ( vector.IsValid()) {
                        result.Add(new AtomPopulationRule(tag, vector));
                    }
                }
            }
            return result;
        }
    }
}
