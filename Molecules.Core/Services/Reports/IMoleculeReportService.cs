using Molecules.Core.Domain.ValueObjects.Reports;

namespace Molecules.Core.Services.Reports
{
    public interface IMoleculeReportService
    {
        Task<List<MoleculeAtomsChargeReport>> GetMoleculeAtomsChargeReportAsync(int moleculeId);

        Task<List<MoleculeAtomOrbitalReport>> GetMoleculeAtomOrbitalReportAsync(int moleculeId);

        Task<List<MoleculeBondsReport>> GetMoleculeBondsReportsAsync(int moleculeId);

        Task<List<MoleculeAtomsPopulationReport>> GetMoleculePopulationReportAsync(int moleculeId);

        Task<List<GeneralMoleculeReport>> GetGeneralMoleculeReportsAsync(int moleculeId);

        Task<List<MoleculeAtomPositionReport>> GetAtomPositionReportAsync(int moleculeId);
    }
}
