using Booking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Booking.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Client> Clients { get; }
    DbSet<Service> Services { get; }
    DbSet<Schedule> Schedules { get; }
    DbSet<Reservation> Reservations { get; }
    DbSet<Payment> Payments { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}