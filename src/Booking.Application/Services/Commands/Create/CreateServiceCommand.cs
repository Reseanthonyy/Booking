using Booking.Application.Common.Interfaces;
using Booking.Domain.Entities;
using Booking.Domain.ValueObjects;
using MediatR;

namespace Booking.Application.Services.Commands.Create;

public record CreateServiceCommand(
    string Name,
    string? Description,
    int DurationMinutes,
    decimal Price,
    string Currency,
    int MaxCapacity) : IRequest<Guid>;

public class CreateServiceCommandHandler
    : IRequestHandler<CreateServiceCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateServiceCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(
        CreateServiceCommand request,
        CancellationToken cancellationToken)
    {
        var duration = TimeSpan.FromMinutes(request.DurationMinutes);
        var price = new Money(request.Price, request.Currency);

        var service = new Service(
            request.Name,
            duration,
            price,
            request.MaxCapacity,
            request.Description);

        _context.Services.Add(service);
        await _context.SaveChangesAsync(cancellationToken);

        return service.Id;
    }
}