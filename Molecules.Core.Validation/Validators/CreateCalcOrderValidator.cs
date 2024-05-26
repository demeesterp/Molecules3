using FluentValidation;
using Molecules.Core.Domain.ValueObjects.Calc.Order.Info;

namespace Molecules.Core.Validation.Validators
{
    public class CreateCalcOrderValidator : AbstractValidator<CreateInfoCalcOrder>
    {
        public CreateCalcOrderValidator()
        {
            RuleFor(item => item.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(item => item.Name).MaximumLength(250).WithMessage("Name cannot be longer than 250 characters");
        }
    }
}
