using FluentValidation;
using Molecules.Core.Domain.ValueObjects.Calc.Order.Info;

namespace Molecules.Core.Validation.Validators
{
    public class UpdateCalcOrderItemValidator : AbstractValidator<UpdateInfoCalcOrderItem>
    {
        public UpdateCalcOrderItemValidator()
        {
            RuleFor(x => x.MoleculeName).NotEmpty().WithMessage("MoleculeName is required");
            RuleFor(x => x.MoleculeName).MaximumLength(250).WithMessage("MoleculeName cannot be longer than 250 characters");
        }
    }
}
