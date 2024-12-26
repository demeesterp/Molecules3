

using Molecules.Core.Domain.ValueObjects.Analysis.Population;

namespace Molecules.Core.Services.Analysis
{
    public interface IMoleculeAnalysisService
    {
        Task<MoleculeAtomPopulationAnalysisResult> DoAtomPopulationAnalysisAsync(int numberOfClusters);

        Task<string> EvaluateNumberOfClusters();

    }
}
