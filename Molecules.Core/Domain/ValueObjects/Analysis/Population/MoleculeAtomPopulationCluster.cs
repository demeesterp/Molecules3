using Molecules.Core.Domain.ValueObjects.AtomData;

namespace Molecules.Core.Domain.ValueObjects.Analysis.Population
{
    public record MoleculeAtomPopulationCluster(Atoms Atom, int ClusterLabel, List<MoleculeAtomPopulationVector> Items)
    {
    }
}
