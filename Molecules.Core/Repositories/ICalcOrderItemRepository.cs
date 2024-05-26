using Molecules.Core.Domain.Entities;

namespace Molecules.Core.Repositories
{
    public interface ICalcOrderItemRepository : IRepository
    {
        Task<CalcOrderItem> CreateAsync(int calcOrderId, int charge, string moleculeName, string calcType, string basisSetCode, string xyz);


        Task<CalcOrderItem> UpdateAsync(int id, int charge, string moleculeName, string calcType, string basisSetCode, string xyz);

        Task DeleteAsync(int id);
    }
}
