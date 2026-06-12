using FluentValidation;

namespace Booking.Application.Schedules.Commands;

public class CreateScheduleCommandValidator
    : AbstractValidator<CreateScheduleCommand>
{
    public CreateScheduleCommandValidator()
    {
        RuleFor(x => x.ServiceId)
            .NotEmpty();

        RuleFor(x => x.Start)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("Schedule must be in the future.");

        RuleFor(x => x.End)
            .GreaterThan(x => x.Start)
            .WithMessage("End must be after Start.");

        RuleFor(x => x)
            .Must(x => (x.End - x.Start).TotalMinutes <= 480)
            .WithMessage("Schedule duration cannot exceed 8 hours.");
    }
}