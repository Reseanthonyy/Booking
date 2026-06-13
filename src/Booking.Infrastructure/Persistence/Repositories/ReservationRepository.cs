using Booking.Domain.Entities;
using Booking.Domain.Enums;
using Booking.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Persistence.Repositories;

public class ReservationRepository : GenericRepository<Reservation>, IReservationRepository
{
    public ReservationRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IReadOnlyList<Reservation>> GetReservationsForScheduleAsync(
        Guid scheduleId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(r => r.ScheduleId == scheduleId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<int> CountConfirmedReservationsForScheduleAsync(
        Guid scheduleId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .CountAsync(r =>
                    r.ScheduleId == scheduleId &&
                    r.Status == BookingStatus.Confirmed,
                cancellationToken);
    }
}