using Booking.Domain.Entities;

namespace Booking.Domain.Interfaces;

public interface IReservationRepository : IRepository<Reservation>
{
    Task<IReadOnlyList<Reservation>> GetReservationsForScheduleAsync(Guid scheduleId, CancellationToken cancellationToken = default);
    Task<int> CountConfirmedReservationsForScheduleAsync(Guid scheduleId, CancellationToken cancellationToken = default);
}