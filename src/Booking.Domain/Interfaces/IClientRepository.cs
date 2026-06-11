using Booking.Domain.Entities;
using Booking.Domain.ValueObjects;

namespace Booking.Domain.Interfaces;

public interface IClientRepository : IRepository<Client>
{
    Task<Client?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default);
}