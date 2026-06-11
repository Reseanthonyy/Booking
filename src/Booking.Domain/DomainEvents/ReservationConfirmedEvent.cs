namespace Booking.Domain.DomainEvents;

public sealed record ReservationConfirmedEvent(Guid ReservationId) : IDomainEvent;