using Booking.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Booking.Application.Reservations.Commands.Confirm;

public record ConfirmReservationCommand(Guid Id) : IRequest;

public class ConfirmReservationCommandHandler
    : IRequestHandler<ConfirmReservationCommand>
{
    private readonly IApplicationDbContext _context;

    public ConfirmReservationCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        ConfirmReservationCommand request,
        CancellationToken cancellationToken)
    {
        var reservation = await _context.Reservations
            .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

        if (reservation is null)
            throw new KeyNotFoundException($"Reservation {request.Id} not found.");

        reservation.Confirm();
        await _context.SaveChangesAsync(cancellationToken);
    }
}