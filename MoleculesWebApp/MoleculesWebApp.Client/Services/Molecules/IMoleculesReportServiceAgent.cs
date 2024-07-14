using MoleculesWebApp.Client.Data.ServiceAgents.Molecules.Report;

namespace MoleculesWebApp.Client.Services.Molecules
{
    public interface IMoleculesReportServiceAgent
    {
        IObservable<List<MoleculeAtomPositionReport>> GetAtomPositionReport(int moleculeid);

        IObservable<List<GeneralMoleculeReport>> GetGeneralReport(int moleculeid);

        IObservable<List<MoleculeAtomOrbitalReport>> GetMoleculeAtomOrbitalReport(int moleculeid);

        IObservable<List<MoleculeAtomsChargeReport>> GetMoleculeAtomsChargeReport(int moleculeid);

        IObservable<List<MoleculeAtomsPopulationReport>> GetMoleculeAtomsPopulationReport(int moleculeid);

        IObservable<List<MoleculeBondsReport>> GetMoleculeBondsReport(int moleculeid);
    }
}
