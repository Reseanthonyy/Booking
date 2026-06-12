namespace Booking.Application.Services.Dtos;

public record ServiceDto(
    Guid Id,
    string Name,
    string? Description,
    string Duration,
    decimal Price,
    string Currency,
    int MaxCapacity,
    bool IsActive);