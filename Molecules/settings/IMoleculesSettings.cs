using Molecules.constants;

namespace Molecules.settings
{
    public interface IMoleculesSettings
    {
        string BasePath { get; }

        string AnalysisOutputPath { get; }

        string ExportPath { get; }

        AppName? App { get; }

        int MoleculeId { get; }

        ReportName Report { get; }
    }
}
