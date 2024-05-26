using Molecules.Core.Domain.Aggregates;
using Molecules.Core.Domain.ValueObjects.Calc.Order.Info;

namespace Molecules.Core.Services.CalcOrders
{
    public interface ICalcOrderService
    {
        /// <summary>
        /// Get the list of all ongoing CalcOrders
        /// </summary>
        /// <returns>A list of CalcOrders</returns>
        public Task<List<CalcOrder>> ListAsync();

        /// <summary>
        /// Get the CalcOrder with the specified id
        /// </summary>
        /// <param name="id">The id of a calculation order</param>
        /// <returns>The requested CalcOrder</returns>
        public Task<CalcOrder> GetAsync(int id);

        /// <summary>
        /// Get the CalcOrders that correspond to the specified name
        /// </summary>
        /// <param name="name">The name or the name pattern to search for</param>
        /// <returns>The corresponding CalcOrders</returns>
        public Task<List<CalcOrder>> GetByNameAsync(string name);

        /// <summary>
        /// Create a CalcOrder with name and description
        /// </summary>
        /// <param name="createInfoCalcOrder">data to create a calcorder</param>
        /// <returns>The created calc order</returns>
        public Task<CalcOrder> CreateAsync(CreateInfoCalcOrder createInfoCalcOrder);

        /// <summary>
        /// Update a CalcOrder with the specified id
        /// </summary>
        /// <param name="id">The id of the CalcOrder to be updated</param>
        /// <param name="updateInfoCalcOrder">data to update a calcOrder</param>
        /// <returns>The updated calcorder</returns>
        public Task<CalcOrder?> UpdateAsync(int id, UpdateInfoCalcOrder updateInfoCalcOrder);

        /// <summary>
        /// Delete the specified CalcOrder
        /// </summary>
        /// <param name="id">The id of the CalcOrder to delete</param>
        /// <returns></returns>
        public Task DeleteAsync(int id);
    }
}
