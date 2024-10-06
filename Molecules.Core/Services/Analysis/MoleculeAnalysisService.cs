using Molecules.Core.Domain.ValueObjects.Analysis;
using Molecules.Core.Services.CalcMolecules;
using Molecules.Shared.Logger;

namespace Molecules.Core.Services.Analysis
{
    public class MoleculeAnalysisService : IMoleculeAnalysisService
    {
        #region dependencies

        private readonly ICalcMoleculeService _calcMoleculeService;


        private readonly IMoleculesLogger _logger;

        #endregion


        public MoleculeAnalysisService(ICalcMoleculeService calcMoleculeService, IMoleculesLogger logger)
        {
            _calcMoleculeService = calcMoleculeService;
            _logger = logger;
        }


        public string CreateDataSet(InputRule rule)
        {
            throw new NotImplementedException();
        }
    }
}
