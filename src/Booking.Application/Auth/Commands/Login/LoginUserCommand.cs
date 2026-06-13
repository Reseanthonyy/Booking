using Booking.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Booking.Application.Auth.Commands.Login;

public record LoginUserCommand(string Username, string Password) : IRequest<AuthResponse>;

public record AuthResponse(Guid UserId, string Username, string Role, string Token);

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AuthResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly ITokenGenerator _tokenGenerator;

    public LoginUserCommandHandler(IApplicationDbContext context, ITokenGenerator tokenGenerator)
    {
        _context = context;
        _tokenGenerator = tokenGenerator;
    }


    public async Task<AuthResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == request.Username && u.IsActive, cancellationToken);
        if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid credentials");

        var token = _tokenGenerator.GenerateToken(user.Id, user.Role);

        return new AuthResponse(user.Id, user.Username, user.Role, token);
    }
}