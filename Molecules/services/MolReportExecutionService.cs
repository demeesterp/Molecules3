using Molecules.constants;
using Molecules.Core.Services.Reports;
using Molecules.settings;
using Molecules.Shared;
using Molecules.Shared.Logger;

namespace Molecules.services
{
    public class MolReportExecutionService
    {

        #region dependencies

        private readonly IMoleculesLogger _logger;

        private readonly IMoleculeReportService _moleculeReportService;

        private readonly IMoleculesSettings _settings;

        #endregion

        public MolReportExecutionService(IMoleculeReportService moleculeReportService,
                                        IMoleculesSettings settings,
                                            IMoleculesLogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _moleculeReportService = moleculeReportService ?? throw new ArgumentNullException(nameof(moleculeReportService));
            _settings = settings ?? throw new ArgumentException(nameof(settings));
        }


        public async Task RunAsync()
        {
            int moleculeid = _settings.MoleculeId;
            if (moleculeid < 0) return;
            switch (_settings.Report)
            {
                case ReportName.Population:
                    var populationReport = await _moleculeReportService.GetMoleculePopulationReportAsync(moleculeid);
                    Console.Write(StringConversion.ToJsonString(populationReport));
                    break;
                case ReportName.Bonds:
                    var bondsReport = await _moleculeReportService.GetMoleculeBondsReportsAsync(moleculeid);
                    Console.Write(StringConversion.ToJsonString(bondsReport));
                    break;
                case ReportName.Charge:
                    var chargeReport = await _moleculeReportService.GetMoleculeAtomsChargeReportAsync(moleculeid);
                    Console.Write(StringConversion.ToJsonString(chargeReport));
                    break;
                case ReportName.AtomOrbital:
                    var atomOrbitalReport = await _moleculeReportService.GetMoleculeAtomOrbitalReportAsync(moleculeid);
                    Console.Write(StringConversion.ToJsonString(atomOrbitalReport));
                    break;
                case ReportName.None:
                default:
                    break;
            }
        }


    }
}
