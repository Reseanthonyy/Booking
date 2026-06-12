using Booking.Application.Common.Interfaces;
using Booking.Domain.Entities;
using Booking.Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Booking.Application.Schedules.Commands;

public record CreateScheduleCommand(
    Guid ServiceId,
    DateTime Start,
    DateTime End,
    int? MaxCapacity) : IRequest<Guid>;

public class CreateScheduleCommandHandler
    : IRequestHandler<CreateScheduleCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateScheduleCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(
        CreateScheduleCommand request,
        CancellationToken cancellationToken)
    {
        // Validar que el servicio existe
        var service = await _context.Services
            .FirstOrDefaultAsync(s => s.Id == request.ServiceId, cancellationToken);

        if (service is null)
            throw new KeyNotFoundException($"Service {request.ServiceId} not found.");

        var timeRange = new TimeRange(request.Start, request.End);

        // Validar que no se empalme con otro horario del mismo servicio
        var hasConflict = await _context.Schedules
            .AnyAsync(s =>
                    s.ServiceId == request.ServiceId &&
                    s.IsActive &&
                    s.TimeRange.Start < timeRange.End &&
                    s.TimeRange.End > timeRange.Start,
                cancellationToken);

        if (hasConflict)
            throw new InvalidOperationException(
                "The schedule conflicts with an existing one for this service.");

        var capacity = request.MaxCapacity ?? service.MaxCapacity;

        var schedule = new Schedule(request.ServiceId, timeRange, capacity);

        _context.Schedules.Add(schedule);
        await _context.SaveChangesAsync(cancellationToken);

        return schedule.Id;
    }
}