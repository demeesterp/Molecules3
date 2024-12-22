using Molecules.Core.Domain.ValueObjects.AtomData;

namespace Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Population
{
    public record MoleculeAtomPopulationCategory(Atoms Atom, int ClusterLabel, List<MoleculeAtomPopulationVector> Items)
    {
    }
}
