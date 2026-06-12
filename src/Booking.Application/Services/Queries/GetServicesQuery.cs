using Booking.Application.Common.Interfaces;
using Booking.Application.Common.Models;
using Booking.Application.Services.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Booking.Application.Services.Queries;

public record GetServicesQuery(
    int PageNumber = 1,
    int PageSize = 20,
    bool? IsActive = null) : IRequest<PaginatedList<ServiceDto>>;

public class GetServicesQueryHandler
    : IRequestHandler<GetServicesQuery, PaginatedList<ServiceDto>>
{
    private readonly IApplicationDbContext _context;

    public GetServicesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<ServiceDto>> Handle(
        GetServicesQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.Services.AsNoTracking();

        if (request.IsActive.HasValue)
            query = query.Where(s => s.IsActive == request.IsActive.Value);

        query = query.OrderBy(s => s.Name);

        var projected = query.ProjectToType<ServiceDto>();

        return await PaginatedList<ServiceDto>.CreateAsync(
            projected,
            request.PageNumber,
            request.PageSize,
            cancellationToken);
    }
}