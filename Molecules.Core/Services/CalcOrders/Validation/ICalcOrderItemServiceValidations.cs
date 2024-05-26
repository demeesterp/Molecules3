using Molecules.Core.Domain.ValueObjects.Calc.Order.Info;

namespace Molecules.Core.Services.CalcOrders.Validation
{
    public interface ICalcOrderItemServiceValidations
    {
        /// <summary>
        /// Validated a CreateCalcOrderItem
        /// </summary>
        /// <param name="createCalcOrderItem">Create calcorderitem</param>
        void Validate(CreateInfoCalcOrderItem createCalcOrderItem);

        /// <summary>
        /// Validated a CreateCalcOrderItem
        /// </summary>
        /// <param name="updateCalcOrderItem">Update calcorderitem</param>
        void Validate(UpdateInfoCalcOrderItem updateCalcOrderItem);
    }
}
