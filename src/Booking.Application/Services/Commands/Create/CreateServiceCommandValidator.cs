using FluentValidation;

namespace Booking.Application.Services.Commands.Create;

public class CreateServiceCommandValidator
    : AbstractValidator<CreateServiceCommand>
{
    public CreateServiceCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().MaximumLength(200);

        RuleFor(x => x.DurationMinutes)
            .GreaterThan(0).WithMessage("Duration must be at least 1 minute.");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Currency)
            .NotEmpty().Length(3);

        RuleFor(x => x.MaxCapacity)
            .GreaterThan(0);
    }
}