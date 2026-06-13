using Booking.Domain.Entities;
using Booking.Domain.Interfaces;
using Booking.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Persistence.Repositories;

public class ClientRepository : GenericRepository<Client>, IClientRepository
{
    
    public ClientRepository(ApplicationDbContext context) : base(context){}
    
    public async Task<Client?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FirstOrDefaultAsync(c => c.Email.Value == email.Value, cancellationToken);
    }
}