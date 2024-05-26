using FluentValidation;
using Molecules.Core.Domain.ValueObjects.Calc.Order.Info;
using Molecules.Core.Services.CalcOrders.Validation;

namespace Molecules.Core.Validation.Services
{
    /// <summary>
    /// Helpers service to group all validations for the CalcOrderService
    /// </summary>
    /// <remarks>
    /// Constructor for CalcOrderServiceValidations
    /// </remarks>
    /// <param name="createCalcOrderValidator">CreateCalcOrder validation logic</param>
    /// <param name="updateCalcOrderValidator">UpdateCalcOrder validation logic</param>
    public class CalcOrderServiceValidations(IValidator<CreateInfoCalcOrder> createCalcOrderValidator,
                                            IValidator<UpdateInfoCalcOrder> updateCalcOrderValidator) : ICalcOrderServiceValidations
    {
        #region dependencies

        private readonly IValidator<CreateInfoCalcOrder> _createCalcOrderValidator = createCalcOrderValidator ?? throw new ArgumentNullException(nameof(createCalcOrderValidator));

        private readonly IValidator<UpdateInfoCalcOrder> _updateCalcOrderValidator = updateCalcOrderValidator ?? throw new ArgumentNullException(nameof(updateCalcOrderValidator));

        #endregion

        /// <summary>
        /// Validate CreateCalcOrder
        /// </summary>
        /// <param name="createCalcOrder">Object to be validated</param>
        /// <exception cref="ValidationException">Throws when validation fails</exception>"
        public void Validate(CreateInfoCalcOrder createCalcOrder)
        {
            _createCalcOrderValidator.ValidateAndThrow(createCalcOrder);
        }

        /// <summary>
        /// Validate UpdateCalcOrder
        /// </summary>
        /// <param name="updateCalcOrder">Object to be validated</param>
        /// <exception cref="ValidationException">Throws when validation fails</exception>"
        public void Validate(UpdateInfoCalcOrder updateCalcOrder)
        {
            _updateCalcOrderValidator.ValidateAndThrow(updateCalcOrder);
        }


    }
}
