

using Molecules.Core.Domain.ValueObjects.Analysis.Population;
using Molecules.Core.Domain.ValueObjects.KMeansAnalysis;

namespace Molecules.Core.Services.Analysis
{
    public interface IMoleculeAnalysisService
    {
        Task<MoleculeAtomPopulationAnalysisResult> DoAtomAnalysisAsync(MoleculeAtomClusteringInput clusters);

    }
}
