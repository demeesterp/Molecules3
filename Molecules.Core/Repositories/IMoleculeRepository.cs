using Molecules.Core.Domain.Entities;

namespace Molecules.Core.Repositories
{
    public interface IMoleculeRepository
    {
        Task<CalcMolecule> CreateAsync(CalcMolecule entity);

        Task<CalcMolecule> UpdateAsync(int id, string moleculeName, string molecule);

        Task DeleteAsync(int id);

        Task<CalcMolecule> GetByIdAsync(int id);

        Task<CalcMolecule?> FindAsync(string orderName, string basisSet, string moleculeName);

        Task<List<CalcMolecule>> FindAllByNameAsync(string moleculeName);

        Task<List<CalcMolecule>> GetAllByNameAsync(string moleculeName);
    }
}
