﻿using Molecules.Core.Domain.ValueObjects.Analysis.AtomRules;
using Molecules.Core.Domain.ValueObjects.Molecules;

namespace Molecules.Core.Factories.Analysis
{
    public interface IRulesCollectionFactory
    {
        AtomPopulationRuleCollection BuildAtomChargeRuleCollection(List<Molecule> molecules);
    }
}
