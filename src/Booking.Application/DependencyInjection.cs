using System.Reflection;
using Booking.Application.Common.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Booking.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        var assembly = Assembly.GetExecutingAssembly();

        serviceCollection.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
        });

        serviceCollection.AddValidatorsFromAssembly(assembly);
        
        serviceCollection.AddTransient(
            typeof(IPipelineBehavior<,>),
            typeof(LogginBehavior<,>));

        return serviceCollection;
    }
}