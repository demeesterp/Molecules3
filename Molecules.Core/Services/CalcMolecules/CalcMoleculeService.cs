using Molecules.Core.Domain.Entities;
using Molecules.Core.Domain.ValueObjects.Molecules;
using Molecules.Core.Repositories;
using Molecules.Shared;
using Molecules.Shared.Logger;

namespace Molecules.Core.Services.CalcMolecules
{
    public class CalcMoleculeService(IMoleculeRepository repository,
                                       IMoleculesLogger logger) : ICalcMoleculeService
    {

        private readonly IMoleculeRepository _repository = repository;

        private readonly IMoleculesLogger _logger = logger;

        public async Task<CalcMolecule> GetAsync(int id)
        {
            _logger.LogInformation($"GetAsync {id}");

            var molecule = await _repository.GetByIdAsync(id);

            return molecule;
        }

        public async Task<CalcMolecule?> FindAsync(string orderName, string basisSet, string moleculeName)
        {
            _logger.LogInformation($"FindAsync for OrderName {orderName}, basisSet {basisSet} moleculeName {moleculeName} ");

            var molecule = await _repository.FindAsync(orderName, basisSet, moleculeName);

            return molecule;
        }

        public async Task<List<CalcMolecule>> FindAllByNameAsync(string moleculeName)
        {
            _logger.LogInformation($"DeleteAsync with id {moleculeName}");

            var moleculeDbEntities = await _repository.FindAllByNameAsync(moleculeName);

            return moleculeDbEntities.OrderByDescending(i => i.OrderName)
                                    .ThenByDescending(i => i.MoleculeName)
                                    .ThenByDescending(i => i.Id).ToList();
        }

        public async Task<CalcMolecule> CreateAsync(CalcMolecule molecule)
        {
            _logger.LogInformation("CreateAsync");

            string moleculeStringData = "";

            if (molecule.Molecule != null)
            {
                moleculeStringData = StringConversion.ToJsonString(molecule.Molecule);
            }

            var moleculeResult = await _repository.CreateAsync(molecule);

            return moleculeResult;
        }

        public async Task DeleteAsync(int id)
        {
            _logger.LogInformation("DeleteAsync");

            await _repository.DeleteAsync(id);
        }

        public async Task<CalcMolecule> UpdateAsync(int id, Molecule? molecule)
        {
            _logger.LogInformation("UpdateAsync");
            ArgumentNullException.ThrowIfNull(molecule);

            var result = await _repository.UpdateAsync(id, molecule.Name, StringConversion.ToJsonString(molecule));

            return result;
        }


    }
}
