using Molecules.Core.Domain.ValueObjects.Calc.Order.Info;

namespace Molecules.Core.Services.CalcOrders.Validation
{
    public interface ICalcOrderServiceValidations
    {
        /// <summary>
        /// Validate CreateCalcOrder
        /// </summary>
        /// <param name="createCalcOrder">Object to be validated</param>
        /// <exception cref="ValidationException">Throws when validation fails</exception>"
        void Validate(CreateInfoCalcOrder createCalcOrder);

        /// <summary>
        /// Validate UpdateCalcOrder
        /// </summary>
        /// <param name="updateCalcOrder">Object to be validated</param>
        /// <exception cref="ValidationException">Throws when validation fails</exception>"
        void Validate(UpdateInfoCalcOrder updateCalcOrder);
    }
}
