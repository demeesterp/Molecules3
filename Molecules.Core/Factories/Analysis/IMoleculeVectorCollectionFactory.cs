using Molecules.Core.Domain.Entities;
using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Population.VectorCollection;

namespace Molecules.Core.Factories.Analysis
{
    public interface IMoleculeVectorCollectionFactory
    {
        List<MoleculeAtomPopulationVectorCollection> CreateMoleculeAtomPopulationVectorCollection(List<CalcMolecule> molecules);

        List<MoleculeAtomPopulationHomoVectorCollection> CreateMoleculeAtomPopulationHomoVectorCollection(List<CalcMolecule> molecules);

        List<MoleculeAtomPopulationLumoVectorCollection> CreateMoleculeAtomPopulationLumoVectorCollection(List<CalcMolecule> molecules);

    }
}
