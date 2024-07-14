using MoleculesWebApp.Client.Data.ServiceAgents.Molecules.Report;
using MoleculesWebApp.Client.Shared.HttpClientHelper;

namespace MoleculesWebApp.Client.Services.Molecules
{
    public class MoleculesReportServiceAgent : IMoleculesReportServiceAgent
    {
        #region dependencies

        private readonly HttpClient _httpClient;

        private readonly MoleculesHttpClient _moleculesHttpClient;

        #endregion


        public MoleculesReportServiceAgent(HttpClient httpClient, MoleculesHttpClient moleculesHttpClient)
        {
            _httpClient = httpClient;
            _moleculesHttpClient = moleculesHttpClient;
        }

        public IObservable<List<MoleculeAtomPositionReport>> GetAtomPositionReport(int moleculeid)
        {
            return _moleculesHttpClient.Get<List<MoleculeAtomPositionReport>>(_httpClient, $"moleculereports/molecule/{moleculeid}/moleculeatompositionreport");
        }

        public IObservable<List<GeneralMoleculeReport>> GetGeneralReport(int moleculeid)
        {
            return _moleculesHttpClient.Get<List<GeneralMoleculeReport>>(_httpClient, $"moleculereports/molecule/{moleculeid}/generalmoleculereport");
        }

        public IObservable<List<MoleculeAtomOrbitalReport>> GetMoleculeAtomOrbitalReport(int moleculeid)
        {
            return _moleculesHttpClient.Get<List<MoleculeAtomOrbitalReport>>(_httpClient, $"moleculereports/molecule/{moleculeid}/atomorbitalreport");
        }

        public IObservable<List<MoleculeAtomsChargeReport>> GetMoleculeAtomsChargeReport(int moleculeid)
        {
            return _moleculesHttpClient.Get<List<MoleculeAtomsChargeReport>>(_httpClient, $"moleculereports/molecule/{moleculeid}/atomchargereport");
        }

        public IObservable<List<MoleculeAtomsPopulationReport>> GetMoleculeAtomsPopulationReport(int moleculeid)
        {
            return _moleculesHttpClient.Get<List<MoleculeAtomsPopulationReport>>(_httpClient, $"moleculereports/molecule/{moleculeid}/moleculepopulationreport");
        }

        public IObservable<List<MoleculeBondsReport>> GetMoleculeBondsReport(int moleculeid)
        {
            return _moleculesHttpClient.Get<List<MoleculeBondsReport>>(_httpClient, $"moleculereports/molecule/{moleculeid}/bondsreport");
        }
    }
}
