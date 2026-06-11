using Booking.Domain.Interfaces;
using Booking.Domain.ValueObjects;

namespace Booking.Domain.Entities;

public sealed class Schedule : IAggregateRoot
{
    public Guid Id { get; private set; }
    public Guid ServiceId { get; private set; }
    public TimeRange TimeRange { get; private set; }
    public int MaxCapacity { get; private set; }
    public bool IsActive { get; private set; }
    
    private Schedule(){}

    public Schedule(Guid serviceId, TimeRange timeRange, int maxCapacity)
    {
        Id = Guid.NewGuid();
        ServiceId = serviceId;
        TimeRange = timeRange;
        MaxCapacity = maxCapacity > 0 ? maxCapacity : throw new ArgumentException("Capacity must be greater than zero");
        IsActive = true;
        
    }
    
    public void Desactivate() => IsActive = false;
    public void Activate() => IsActive = true;
}