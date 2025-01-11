using FluentValidation;
using ShippingOrderService.Application.Commands.CreateShippingOrder;

namespace SHOService.Application.Validators;

public partial class CreateShippingOrderCommandValidator : AbstractValidator<CreateShippingOrderCommand>
{
    public CreateShippingOrderCommandValidator()
    {
        RuleFor(x => x.TrackingNumber)
            .NotEmpty().WithMessage("Tracking number is required.")
            .Length(5, 30).WithMessage("Tracking number must be between 5 and 30 characters.");

        RuleFor(x => x.ShippingDate)
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Shipping date cannot be in the future.");
    }
}
