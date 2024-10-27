using Molecules.Core.Domain.Entities;
using Molecules.Core.Domain.ValueObjects.Molecules;
using Molecules.Core.Services.CalcMolecules;
using Molecules.Shared.Logger;

namespace Molecules.Core.Services.Analysis
{
    public class MoleculeAnalysisService(ICalcMoleculeService calcMoleculeService, IMoleculesLogger logger) : IMoleculeAnalysisService
    {
        #region dependencies

        private readonly ICalcMoleculeService _calcMoleculeService = calcMoleculeService;


        private readonly IMoleculesLogger _logger = logger;

        #endregion

        private async Task<List<Molecule>> GetAllMoleculesAsync()
        {
            List<Molecule> moleculeList = new List<Molecule>();
            var molecules = await _calcMoleculeService.FindAllByNameAsync("%");
            foreach (CalcMolecule mol in molecules)
            {
                CalcMolecule fullResult = await _calcMoleculeService.GetAsync(mol.Id);
                if (fullResult.Molecule is not null)
                {
                    moleculeList.Add(fullResult.Molecule);
                }
            }
            return moleculeList;
        }


        // Tasks of this service 
        // Task 1 Get the molecules from DataBase
        // Task 2 Build a vector collections
        // Task 3 Cluster the vectors around centroids
        // Task 4 Build a clustering report



    }
}
