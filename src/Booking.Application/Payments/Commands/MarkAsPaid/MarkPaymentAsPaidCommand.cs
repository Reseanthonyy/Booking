using Booking.Application.Common.Interfaces;
using Booking.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Booking.Application.Payments.Commands.MarkAsPaid;

public record MarkPaymentAsPaidCommand(
    Guid Id,
    string Method,
    string? TransactionId = null) : IRequest;

public class MarkPaymentAsPaidCommandHandler
    : IRequestHandler<MarkPaymentAsPaidCommand>
{
    private readonly IApplicationDbContext _context;

    public MarkPaymentAsPaidCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        MarkPaymentAsPaidCommand request,
        CancellationToken cancellationToken)
    {
        var payment = await _context.Payments
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (payment is null)
            throw new KeyNotFoundException($"Payment {request.Id} not found.");

        if (!Enum.TryParse<PaymentMethod>(request.Method, true, out var method))
            throw new ArgumentException($"Invalid payment method: {request.Method}");

        payment.MarkAsPaid(method, request.TransactionId);
        await _context.SaveChangesAsync(cancellationToken);
    }
}