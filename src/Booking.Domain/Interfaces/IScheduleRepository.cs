using Booking.Domain.Entities;
using Booking.Domain.ValueObjects;

namespace Booking.Domain.Interfaces;

public interface IScheduleRepository : IRepository<Schedule>
{
    Task<IReadOnlyList<Schedule>> GetScheduleForServiceAsync(Guid serviceId, CancellationToken cancellationToken = default);
    Task<bool> HasConflictingScheduleAsync(Guid serviceId, TimeRange timeRange, CancellationToken cancellationToken = default);
}