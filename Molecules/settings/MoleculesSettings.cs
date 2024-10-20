using Microsoft.Extensions.Configuration;
using Molecules.constants;

namespace Molecules.settings
{
    public class MoleculesSettings : IMoleculesSettings
    {
        #region dependencies

        private readonly IConfiguration _configuration;

        private const string basePath = nameof(basePath);

        private const string appName = nameof(appName);

        private const string moleculeid = nameof(MoleculeId);

        private const string analysisoutputpath = nameof(analysisoutputpath);

        private const string reporttype = "ReportType";

        private const string exportpath = nameof(moleculeid);

        #endregion

        public MoleculesSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string BasePath => _configuration[basePath] ?? Directory.GetCurrentDirectory();

        public string AnalysisOutputPath => Path.Combine(BasePath, _configuration[analysisoutputpath] ?? string.Empty);

        public AppName? App => Enum.TryParse(_configuration[appName], true, out AppName result) ? result : null;

        public int MoleculeId => int.TryParse(_configuration[moleculeid], out int result) ? result : -1;

        public ReportName Report => Enum.TryParse(_configuration[reporttype], true, out ReportName result) ? result : ReportName.None;

        public string ExportPath => _configuration[exportpath]?? Directory.GetCurrentDirectory();
    }
}
