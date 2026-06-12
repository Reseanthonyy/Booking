namespace Booking.Application.Schedules.Dtos;

public record ScheduleDto(
    Guid Id,
    Guid ServiceId,
    string ServiceName,
    DateTime Start,
    DateTime End,
    int MaxCapacity,
    int ConfirmedReservations,
    bool IsActive);