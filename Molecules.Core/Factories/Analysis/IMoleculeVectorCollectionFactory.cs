using Molecules.Core.Domain.Entities;
using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Orbital.VectorCollections;
using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Population.VectorCollection;

namespace Molecules.Core.Factories.Analysis
{
    public interface IMoleculeVectorCollectionFactory
    {
        List<MoleculeAtomPopulationVectorCollection> CreateMoleculeAtomPopulationVectorCollection(List<CalcMolecule> molecules);

        List<MoleculeAtomHomoPopulationVectorCollection> CreateMoleculeAtomPopulationHomoVectorCollection(List<CalcMolecule> molecules);

        List<MoleculeAtomLumoPopulationVectorCollection> CreateMoleculeAtomPopulationLumoVectorCollection(List<CalcMolecule> molecules);

        List<MoleculeAtomOrbitalPopulationVectorCollection> CreateMoleculeAtomOrbitalPopulationVectorCollection(List<CalcMolecule> molecules);

        List<MoleculeAtomOrbitalHomoPopulationVectorCollection> CreateMoleculeAtomOrbitalHomoPopulationVectorCollection(List<CalcMolecule> molecules);

        List<MoleculeAtomOrbitalLumoPopulationVectorCollection> CreateMoleculeAtomOrbitalLumoPopulationVectorCollection(List<CalcMolecule> molecules);

    }
}
