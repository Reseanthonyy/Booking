using Booking.Domain.Interfaces;
using Booking.Domain.ValueObjects;

namespace Booking.Domain.Entities;

public sealed class Service : IAggregateRoot
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public TimeSpan Duration { get; private set; }
    public Money Price { get; private set; }
    public int MaxCapacity { get; private set; }
    public bool IsActive { get; private set; }
    
    public Service(){}

    public Service(string name, TimeSpan duration, Money price, int maxCapacity, string description)
    {
        Id = Guid.NewGuid();
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Duration = duration;
        Price = price;
        MaxCapacity = maxCapacity > 0 ? maxCapacity : throw new ArgumentException(nameof(maxCapacity));
        IsActive = true;
    }
    
    public void Desactivate() => IsActive = false;
    public void Activate() => IsActive = true;
}