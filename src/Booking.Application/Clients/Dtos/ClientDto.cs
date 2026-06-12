namespace Booking.Application.Clients.Dtos;

public record ClientDto(Guid Id, 
    string Name, 
    string Email, 
    string Phone, 
    DateTime CreatedAt);