using Booking.Application.Common.Interfaces;
using Booking.Domain.Entities;
using Booking.Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Booking.Application.Payments.Commands.Register;

public record RegisterPaymentCommand(
    Guid ReservationId,
    decimal Amount,
    string Currency,
    DateTime DueDate) : IRequest<Guid>;

public class RegisterPaymentCommandHandler
    : IRequestHandler<RegisterPaymentCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public RegisterPaymentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(
        RegisterPaymentCommand request,
        CancellationToken cancellationToken)
    {
        var reservationExists = await _context.Reservations
            .AnyAsync(r => r.Id == request.ReservationId, cancellationToken);

        if (!reservationExists)
            throw new KeyNotFoundException(
                $"Reservation {request.ReservationId} not found.");

        var money = new Money(request.Amount, request.Currency);

        var payment = new Payment(
            request.ReservationId,
            money,
            request.DueDate);

        _context.Payments.Add(payment);
        await _context.SaveChangesAsync(cancellationToken);

        return payment.Id;
    }
}