using Molecules.Core.Domain.Entities;
using Molecules.Core.Domain.ValueObjects.Molecules;

namespace Molecules.Core.Services.CalcMolecules
{
    public interface ICalcMoleculeService
    {
        Task<CalcMolecule> GetAsync(int id);

        Task<CalcMolecule?> FindAsync(string orderName, string basisSet, string moleculeName);

        Task<List<CalcMolecule>> FindAllByNameAsync(string moleculeName);

        Task<CalcMolecule> CreateAsync(CalcMolecule molecule);

        Task<CalcMolecule> UpdateAsync(int id, Molecule? molecule);

        Task DeleteAsync(int id);


    }
}
