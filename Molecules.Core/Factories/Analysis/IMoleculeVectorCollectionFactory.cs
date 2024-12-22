using Molecules.Core.Domain.Entities;
using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Population;

namespace Molecules.Core.Factories.Analysis
{
    public interface IMoleculeVectorCollectionFactory
    {
        List<MoleculeAtomPopulationVectorCollection> CreateMoleculeAtomPopulationVectorCollection(List<CalcMolecule> molecules);

    }
}
