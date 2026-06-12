namespace Booking.Application.Payments.Dtos;

public record PaymentDto(
    Guid Id,
    Guid ReservationId,
    string ClientName,
    string ServiceName,
    decimal Amount,
    string Currency,
    DateTime DueDate,
    DateTime? PaidDate,
    string Status,
    string Method,
    string? TransactionId);