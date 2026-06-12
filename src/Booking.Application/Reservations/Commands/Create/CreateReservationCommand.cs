using Booking.Application.Common.Interfaces;
using Booking.Domain.Entities;
using Booking.Domain.Enums;
using Booking.Domain.Execptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Booking.Application.Reservations.Commands.Create;

public record CreateReservationCommand(
    Guid ClientId,
    Guid ScheduleId,
    string? Notes = null) : IRequest<Guid>;

public class CreateReservationCommandHandler
    : IRequestHandler<CreateReservationCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateReservationCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(
        CreateReservationCommand request,
        CancellationToken cancellationToken)
    {
        // Validar cliente
        var clientExists = await _context.Clients
            .AnyAsync(c => c.Id == request.ClientId, cancellationToken);
        if (!clientExists)
            throw new KeyNotFoundException($"Client {request.ClientId} not found.");

        // Validar horario
        var schedule = await _context.Schedules
            .FirstOrDefaultAsync(s => s.Id == request.ScheduleId, cancellationToken);
        if (schedule is null)
            throw new KeyNotFoundException($"Schedule {request.ScheduleId} not found.");
        if (!schedule.IsActive)
            throw new InvalidOperationException("Schedule is not active.");

        // Validar capacidad
        var confirmedCount = await _context.Reservations
            .CountAsync(r =>
                r.ScheduleId == request.ScheduleId &&
                r.Status == BookingStatus.Confirmed,
                cancellationToken);

        if (confirmedCount >= schedule.MaxCapacity)
            throw new CapacityExceededException(request.ScheduleId);

        // Validar que no tenga ya una reserva pendiente en ese horario
        var existingReservation = await _context.Reservations
            .AnyAsync(r =>
                r.ClientId == request.ClientId &&
                r.ScheduleId == request.ScheduleId &&
                r.Status != BookingStatus.Cancelled,
                cancellationToken);

        if (existingReservation)
            throw new InvalidOperationException(
                "Client already has an active reservation for this schedule.");

        var reservation = new Reservation(
            request.ClientId,
            request.ScheduleId,
            request.Notes);

        _context.Reservations.Add(reservation);
        await _context.SaveChangesAsync(cancellationToken);

        return reservation.Id;
    }
}