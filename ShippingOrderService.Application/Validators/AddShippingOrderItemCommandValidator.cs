using FluentValidation;
using ShippingOrderService.Application.Commands.AddShippingOrderItem;

namespace SHOService.Application.Validators;

public partial class CreateShippingOrderCommandValidator
{
    public class AddShippingOrderItemCommandValidator : AbstractValidator<AddShippingOrderItemCommand>
    {
        public AddShippingOrderItemCommandValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .Length(3, 100).WithMessage("Description must be between 3 and 100 characters.");

            RuleFor(x => x.Weight)
                .GreaterThan(0).WithMessage("Weight must be greater than zero.");
        }
    }
}
