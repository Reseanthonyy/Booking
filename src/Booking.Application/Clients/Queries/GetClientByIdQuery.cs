using Booking.Application.Clients.Commands.Delete;
using Booking.Application.Clients.Dtos;
using Booking.Application.Common.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Booking.Application.Clients.Queries;

public record GetClientByIdQuery(Guid Id) : IRequest<ClientDto?>;

public class GetClientByIdQueryHandler
    : IRequestHandler<GetClientByIdQuery, ClientDto?>
{
    private readonly IApplicationDbContext _context;

    public GetClientByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ClientDto?> Handle(
        GetClientByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Clients
            .AsNoTracking()
            .ProjectToType<ClientDto>()
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
    }
}