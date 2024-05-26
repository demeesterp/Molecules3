using FluentValidation;
using Molecules.Core.Domain.ValueObjects.Calc.Order.Info;
using Molecules.Core.Services.CalcOrders.Validation;

namespace Molecules.Core.Validation.Services
{
    /// <summary>
    /// Helper service to group all validations for the CalcOrderItemService
    /// </summary>
    /// <remarks>
    /// Constructor
    /// </remarks>
    /// <param name="createValidator">Injected validator from fluent validations</param>
    /// <param name="updateValidator">Injected validator from fluent validations</param>
    /// <exception cref="ArgumentNullException">validator should not be null</exception>
    public class CalcOrderItemServiceValidations(IValidator<CreateInfoCalcOrderItem> createValidator,
                                                    IValidator<UpdateInfoCalcOrderItem> updateValidator) : ICalcOrderItemServiceValidations
    {
        #region dependencies

        private readonly IValidator<CreateInfoCalcOrderItem> _createCalcOrderItemValidator = createValidator;

        private readonly IValidator<UpdateInfoCalcOrderItem> _updateCalcOrderItemValidator = updateValidator;

        #endregion


        /// <inheritdoc/>
        public void Validate(CreateInfoCalcOrderItem createCalcOrderItem)
        {
            _createCalcOrderItemValidator.ValidateAndThrow(createCalcOrderItem);
        }

        /// <inheritdoc/>
        public void Validate(UpdateInfoCalcOrderItem updateCalcOrderItem)
        {
            _updateCalcOrderItemValidator.ValidateAndThrow(updateCalcOrderItem);
        }
    }
}
