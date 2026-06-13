using Booking.Application.Common.Interfaces;
using Booking.Domain.Interfaces;
using Booking.Infrastructure.Persistence;
using Booking.Infrastructure.Persistence.Repositories;
using Booking.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Booking.Infrastructure;

public static class DependenyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        serviceCollection.AddScoped<IApplicationDbContext>(provider =>
            provider.GetRequiredService<ApplicationDbContext>());
        
        //Repositorios
        serviceCollection.AddScoped<IClientRepository, ClientRepository>();
        serviceCollection.AddScoped<IServiceRepository, ServiceRepository>();
        serviceCollection.AddScoped<IScheduleRepository, ScheduleRepository>();
        serviceCollection.AddScoped<IReservationRepository, ReservationRepository>();
        serviceCollection.AddScoped<IPaymentRepository, PaymentRepository>();

        // Servicios
        serviceCollection.AddScoped<ICurrentUserService, CurrentUserService>();
        serviceCollection.AddScoped<ITokenGenerator, JwtTokenGenerator>();

        // HttpContextAccessor
        serviceCollection.AddHttpContextAccessor();

        return serviceCollection;
    }
}