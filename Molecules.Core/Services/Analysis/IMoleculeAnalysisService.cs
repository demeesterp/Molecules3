
using Molecules.Core.Domain.ValueObjects.Analysis;

namespace Molecules.Core.Services.Analysis
{
    public interface IMoleculeAnalysisService
    {
        Task<string> CreateDataSetAsync(AnalysisTypeEnum rule);

    }
}
