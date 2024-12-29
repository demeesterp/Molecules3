using Molecules.Core.Domain.ValueObjects.KMeansAnalysis;
using Molecules.Core.Domain.ValueObjects.KMeansAnalysis.Base.Result;

namespace Molecules.Core.Services.Analysis
{
    public interface IMoleculeAnalysisService
    {
        Task<IMoleculeAnalysisResult> DoAtomAnalysisAsync(MoleculeAtomClusteringInput clusters);

    }
}
