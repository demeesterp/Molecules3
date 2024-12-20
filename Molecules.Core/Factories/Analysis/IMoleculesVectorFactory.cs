﻿using Molecules.Core.Domain.ValueObjects.Analysis.Population;
using Molecules.Core.Domain.ValueObjects.Molecules;

namespace Molecules.Core.Factories.Analysis
{
    public interface IMoleculesVectorFactory
    {
        MoleculeAtomPopulationVector CreateMoleculeAtomPopulationVector(Atom atom, string moleculeName);

    }
}
