using Molecules.Core.Domain.ValueObjects.Analysis;
using Molecules.Core.Domain.ValueObjects.Molecules;
using Molecules.Core.Factories.Analysis;
using Molecules.Core.Services.CalcMolecules;
using Molecules.Shared.Logger;

namespace Molecules.Core.Services.Analysis
{
    public class MoleculeAnalysisService(ICalcMoleculeService calcMoleculeService, IRulesCollectionFactory rulesCollectionFactory, IMoleculesLogger logger) : IMoleculeAnalysisService
    {
        #region dependencies

        private readonly ICalcMoleculeService _calcMoleculeService = calcMoleculeService;

        private readonly IRulesCollectionFactory _rulesCollectionFactory = rulesCollectionFactory;

        private readonly IMoleculesLogger _logger = logger;

        #endregion


        public async Task<string> CreateDataSetAsync(AnalysisTypeEnum rule)
        {
            string result = string.Empty;
            var molecules = await _calcMoleculeService.FindAllByNameAsync("%");
            List<Molecule> moleculeList = molecules.FindAll(x => x.Molecule is not null).ConvertAll(x => x.Molecule!);
            switch (rule)
            {
                case AnalysisTypeEnum.AtomChargeRules:
                    result  = _rulesCollectionFactory.BuildAtomChargeRuleCollection(moleculeList).ToString();
                    break;
                default:
                    break;
            }
            return result;
        }
    }
}
