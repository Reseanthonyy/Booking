using Booking.Domain.Entities;

namespace Booking.Domain.Interfaces;

public interface IPaymentRepository : IRepository<Payment>
{
    Task<IReadOnlyList<Payment>> GetPaymentsForReservationAsync(Guid reservationId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Payment>> GetOverduePaymentsAsync(CancellationToken cancellationToken = default);
}