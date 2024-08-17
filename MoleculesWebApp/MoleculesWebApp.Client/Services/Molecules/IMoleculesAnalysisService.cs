using MoleculesWebApp.Client.Data.Model.Molecule;

namespace MoleculesWebApp.Client.Services.Molecules
{
    public interface IMoleculesAnalysisService
    {
        IObservable<List<MoleculesOverviewItemModel>> SearchCalculatedMolecules(string findquery);

        IObservable<MoleculeReportModel> GetReport(int moleculeId);

    }
}
