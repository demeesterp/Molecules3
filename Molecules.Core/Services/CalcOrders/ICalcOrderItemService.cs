using Molecules.Core.Domain.Entities;
using Molecules.Core.Domain.ValueObjects.Calc.Order.Info;

namespace Molecules.Core.Services.CalcOrders
{
    public interface ICalcOrderItemService
    {
        /// <summary>
        /// Create a new CalcOrder Item for a given CalcOrder
        /// </summary>
        /// <param name="calcOrderId">Id of the calcOrder</param>
        /// <param name="calcOrderItem">The item to be created</param>
        /// <returns>The created calcorderitem</returns>
        Task<CalcOrderItem> CreateAsync(int calcOrderId, CreateInfoCalcOrderItem calcOrderItem);

        /// <summary>
        /// Update a CalcOrderItem with a given id
        /// </summary>
        /// <param name="id">id of the calcorder item to update</param>
        /// <param name="calcOrderItem">The parameters to update</param>
        /// <returns>The modified item</returns>
        Task<CalcOrderItem> UpdateAsync(int id, UpdateInfoCalcOrderItem calcOrderItem);

        /// <summary>
        /// Delete a CalcOrderItem with a given id
        /// </summary>
        /// <param name="id">Id of the calcorder item</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
