using Molecules.Core.Domain.ValueObjects.Molecules;
using Molecules.Core.Domain.ValueObjects.Reports;

namespace Molecules.Core.Factories.Reports
{
    public interface IMoleculeReportFactory
    {
        List<GeneralMoleculeReport> GetGeneralMoleculeReport(Molecule? molecule);
        List<MoleculeAtomsPopulationReport> GetMoleculePopulationReport(Molecule? molecule);

        List<MoleculeAtomsChargeReport> GetMoleculeAtomsChargeReport(Molecule? molecule);

        List<MoleculeBondsReport> GetMoleculeBondsReports(Molecule? molecule);

        List<MoleculeAtomOrbitalReport> GetMoleculeAtomOrbitalReport(Molecule? molecule);

        List<MoleculeAtomPositionReport> GetAtomPositionReport(Molecule? molecule);


    }
}
