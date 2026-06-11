using Booking.Domain.DomainEvents;
using Booking.Domain.Enums;
using Booking.Domain.Interfaces;

namespace Booking.Domain.Entities;

public sealed class Reservation : IAggregateRoot
{
    public Guid Id { get; private set; }
    public Guid ClientId { get; private set; }
    public Guid ScheduleId { get; private set; }
    public BookingStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string? Notes {get; private set;}

    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    private Reservation(Guid clientId, Guid scheduleId, string? notes = null)
    {
        Id = Guid.NewGuid();
        ClientId = clientId;
        ScheduleId = scheduleId;
        Status = BookingStatus.Pending;
        CreatedAt = DateTime.UtcNow;
        Notes = notes;
    }

    public void Confirm()
    {
        if (Status != BookingStatus.Pending)
            throw new ArgumentException("Only pending reservation can be confimed");
        Status = BookingStatus.Confirmed;
    }

    public void Cancel()
    {
        if(Status == BookingStatus.Completed || Status == BookingStatus.Cancelled )
            throw new ArgumentException("Only confirmed reservation can be cancelled");
        Status = BookingStatus.Cancelled;
    }

    public void MarkAsCompleted()
    {
        if(Status != BookingStatus.Confirmed)
            throw new ArgumentException("Only confirmed reservation can be marked as completed");
        Status = BookingStatus.Completed;
    }
}