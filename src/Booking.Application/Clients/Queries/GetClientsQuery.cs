using Booking.Application.Clients.Dtos;
using Booking.Application.Common.Interfaces;
using Booking.Application.Common.Models;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Booking.Application.Clients.Queries;

public record GetClientsQuery(int pageNumber = 1, int pageSize = 20, string? searchTerm = null) : IRequest<PaginatedList<ClientDto>>;

public class GetClientsQueryHandler : IRequestHandler<GetClientsQuery, PaginatedList<ClientDto>>
{
    private readonly IApplicationDbContext _context;

    public GetClientsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<ClientDto>> Handle(GetClientsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Clients.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(request.searchTerm))
        {
            var term = request.searchTerm.ToLower();
            query = query.Where(c =>
                c.Name.ToLower().Contains(term) ||
                c.Email.Value.ToLower().Contains(term));
        }

        query = query.OrderBy(c => c.Name);

        var projectedQuery = query.ProjectToType<ClientDto>();

        return await PaginatedList<ClientDto>.CreateAsync(
            projectedQuery,
            request.pageNumber,
            request.pageSize,
            cancellationToken);
    }
}