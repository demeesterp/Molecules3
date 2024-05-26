using Molecules.Core.Domain.Aggregates;

namespace Molecules.Core.Repositories
{
    public interface ICalcOrderRepository : IRepository
    {
        Task<CalcOrder> CreateAsync(string name, string description);

        Task<CalcOrder> UpdateAsync(int id, string name, string description);

        Task DeleteAsync(int id);

        Task<CalcOrder> GetByIdAsync(int id);

        Task<List<CalcOrder>> GetAllAsync();

        Task<List<CalcOrder>> GetByNameAsync(string name);
    }
}
