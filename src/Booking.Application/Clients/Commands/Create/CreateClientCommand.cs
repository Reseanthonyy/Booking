using Booking.Application.Common.Interfaces;
using Booking.Domain.Entities;
using Booking.Domain.ValueObjects;
using MediatR;

namespace Booking.Application.Clients.Commands.Create;

public record CreateClientCommand(string Name, string Email, string Phone) : IRequest<Guid>;

public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateClientCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        var email = new Email(request.Email);
        var phone = new PhoneNumber(request.Phone);

        var client = new Client(request.Name, email, phone);

        _context.Clients.Add(client);
        await _context.SaveChangesAsync(cancellationToken);

        return client.Id;
    }
}