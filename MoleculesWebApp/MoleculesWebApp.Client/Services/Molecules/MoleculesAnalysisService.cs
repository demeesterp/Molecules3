using MoleculesWebApp.Client.Data.Model;
using System.Reactive.Linq;
using MoleculesWebApp.Client.Shared.Error;
using MoleculesWebApp.Client.Data.ServiceAgents.Molecules.Report;
using MoleculesWebApp.Client.Data.ServiceAgents.Molecules;


namespace MoleculesWebApp.Client.Services.Molecules
{
    public class MoleculesAnalysisService : IMoleculesAnalysisService
    {
        #region dependencies

        private readonly IMoleculesServiceAgent _moleculesServiceAgent;

        private readonly IMoleculesReportServiceAgent _moleculesReportServiceAgent;

        private readonly ErrorHandlingService _errorHandlingService;

        #endregion


        public MoleculesAnalysisService(IMoleculesServiceAgent moleculesServiceAgent,
                                            IMoleculesReportServiceAgent moleculesReportServiceAgent,
                                            ErrorHandlingService errorHandlingService)
        {
            _moleculesServiceAgent = moleculesServiceAgent;
            _errorHandlingService = errorHandlingService;
            _moleculesReportServiceAgent = moleculesReportServiceAgent;
        }


        public IObservable<List<MoleculesOverviewItemModel>> SearchCalculatedMolecules(string findquery)
        {
            return _moleculesServiceAgent.
                    GetByName(findquery)
                    .Catch<IList<CalcMolecule>, Exception>(HandleError<List<CalcMolecule>>)
                    .Select(result => result.Select(resultItem => new MoleculesOverviewItemModel(resultItem)).ToList());
        }


        public IObservable<MoleculeReportModel> GetReport(int moleculeId)
        {
            var generalReportObservable = _moleculesReportServiceAgent.GetGeneralReport(moleculeId)
                            .Catch<List<GeneralMoleculeReport>, Exception>(HandleError<List<GeneralMoleculeReport>>);


            var atomPositionObservable = _moleculesReportServiceAgent.GetAtomPositionReport(moleculeId)
                            .Catch<List<MoleculeAtomPositionReport>, Exception>(HandleError<List<MoleculeAtomPositionReport>>);


            var moleculeBondObservable = _moleculesReportServiceAgent.GetMoleculeBondsReport(moleculeId)
                            .Catch<List<MoleculeBondsReport>, Exception>(HandleError<List<MoleculeBondsReport>>);


            return generalReportObservable.CombineLatest(atomPositionObservable, moleculeBondObservable,
                    (generalReport, atomPosition, moleculeBond) => new MoleculeReportModel(moleculeId)
                    {
                        ReportItems = generalReport.ConvertAll(item => new GeneralMoleculeReportItemModel(item)),
                        AtomPositions = atomPosition.ConvertAll(item => new MoleculeAtomPositionReportItemModel(item)),
                        MoleculeBonds = moleculeBond.ConvertAll(item => new MoleculeBondsReportItemModel(item))
                    });
        }

        #region private helpers


        private IObservable<ReturnType> HandleError<ReturnType>(Exception ex) where ReturnType : new()
        {
            _errorHandlingService.NotifyHttpError(ex);
            return Observable.Return(new ReturnType());
        }

        #endregion


    }
}
