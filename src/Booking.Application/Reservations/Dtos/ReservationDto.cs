namespace Booking.Application.Reservations.Dtos;

public record ReservationDto(
    Guid Id,
    Guid ClientId,
    string ClientName,
    Guid ScheduleId,
    DateTime Start,
    DateTime End,
    string ServiceName,
    string Status,
    DateTime CreatedAt,
    string? Notes);