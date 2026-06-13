using System.Security.Claims;
using Booking.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Booking.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? UserId
    {
        get
        {
            var userId = _httpContextAccessor.HttpContext?.User?
                .FindFirstValue(ClaimTypes.NameIdentifier);
            
            return Guid.TryParse(userId, out var parsed) ? parsed : null;
        }
    }
    
    public string? Role =>
        _httpContextAccessor.HttpContext?.User?
            .FindFirstValue(ClaimTypes.Role);

    public bool IsAuthenticated =>
        _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
}