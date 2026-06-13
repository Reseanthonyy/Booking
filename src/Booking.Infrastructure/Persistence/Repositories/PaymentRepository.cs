using Booking.Domain.Entities;
using Booking.Domain.Enums;
using Booking.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Persistence.Repositories;

public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
{
    public PaymentRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IReadOnlyList<Payment>> GetPaymentsForReservationAsync(
        Guid reservationId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(p => p.ReservationId == reservationId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Payment>> GetOverduePaymentsAsync(
        CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;
        return await _dbSet
            .Where(p => p.Status == PaymentStatus.Pending && p.DueDate < now)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}