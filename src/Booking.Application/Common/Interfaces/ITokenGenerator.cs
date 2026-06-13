namespace Booking.Application.Common.Interfaces;

public interface ITokenGenerator
{
    string GenerateToken(Guid userId, string role);
}