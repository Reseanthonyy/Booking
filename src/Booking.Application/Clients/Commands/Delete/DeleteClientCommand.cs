using Booking.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Booking.Application.Clients.Commands.Delete;

public record DeleteClientCommand(Guid Id) : IRequest;

public class DeleteClientCommandHandler
    : IRequestHandler<DeleteClientCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteClientCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        DeleteClientCommand request,
        CancellationToken cancellationToken)
    {
        var client = await _context.Clients
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (client is null)
            throw new KeyNotFoundException($"Client {request.Id} not found.");

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync(cancellationToken);
    }
}