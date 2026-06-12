using Booking.Application.Common.Interfaces;
using Booking.Application.Common.Models;
using Booking.Application.Payments.Dtos;
using Booking.Domain.Enums;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Booking.Application.Payments.Queries.GetOverdue;

public record GetOverduePaymentsQuery(
    int PageNumber = 1,
    int PageSize = 20) : IRequest<PaginatedList<PaymentDto>>;

public class GetOverduePaymentsQueryHandler
    : IRequestHandler<GetOverduePaymentsQuery, PaginatedList<PaymentDto>>
{
    private readonly IApplicationDbContext _context;

    public GetOverduePaymentsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<PaymentDto>> Handle(
        GetOverduePaymentsQuery request,
        CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;

        var query = _context.Payments
            .AsNoTracking()
            .Where(p =>
                p.Status == PaymentStatus.Pending &&
                p.DueDate < now)
            .OrderBy(p => p.DueDate);

        var projected = query.ProjectToType<PaymentDto>();

        return await PaginatedList<PaymentDto>.CreateAsync(
            projected,
            request.PageNumber,
            request.PageSize,
            cancellationToken);
    }
}