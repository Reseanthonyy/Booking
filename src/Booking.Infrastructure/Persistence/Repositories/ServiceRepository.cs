using Booking.Domain.Entities;
using Booking.Domain.Interfaces;

namespace Booking.Infrastructure.Persistence.Repositories;

public class ServiceRepository : GenericRepository<Service>, IServiceRepository
{
    public ServiceRepository(ApplicationDbContext context) : base(context) { }
}