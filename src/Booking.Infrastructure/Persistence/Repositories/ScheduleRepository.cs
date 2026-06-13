using Booking.Domain.Entities;
using Booking.Domain.Interfaces;
using Booking.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Persistence.Repositories;

public class ScheduleRepository : GenericRepository<Schedule>, IScheduleRepository
{
    public ScheduleRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IReadOnlyList<Schedule>> GetSchedulesForServiceAsync(
        Guid serviceId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(s => s.ServiceId == serviceId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
    
    public async Task<bool> HasConflictingScheduleAsync(
        Guid serviceId, TimeRange timeRange, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .AnyAsync(s =>
                    s.ServiceId == serviceId &&
                    s.IsActive &&
                    s.TimeRange.Start < timeRange.End &&
                    s.TimeRange.End > timeRange.Start,
                cancellationToken);
    }
}