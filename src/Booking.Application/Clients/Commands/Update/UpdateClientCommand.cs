using Booking.Application.Common.Interfaces;
using Booking.Domain.Entities;
using Booking.Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Booking.Application.Clients.Commands.Update;

public record UpdateClientCommand(Guid Id, string Name, string Email, string Phone) : IRequest;

public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateClientCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }


    public async Task Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        var client = await _context.Clients
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (client is null)
            throw new KeyNotFoundException($"Client {request.Id} not found");
        
        client.UpdateContactInfo(
            email:new Email(request.Email),
            phoneNumber:new PhoneNumber(request.Phone));
        
        typeof(Client)
            .GetProperty(nameof(client.Name))!
            .SetValue(client, request.Name);

        await _context.SaveChangesAsync(cancellationToken);
    }
}