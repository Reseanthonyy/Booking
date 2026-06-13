using Booking.Application.Common.Interfaces;
using Booking.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Booking.Application.Auth.Commands.Register;

public record RegisterUserCommand(string Username, string Password, string Role) : IRequest<Guid>;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public RegisterUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        //Valida que no exista el usuario
        if(await _context.Users.AnyAsync(u => u.Username == request.Username, cancellationToken))
            throw new InvalidOperationException("Username already exists");
        
        //Hash de contraseña simple (en produccion usar bcrypt)
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var user = new User(request.Username, passwordHash, request.Role);

        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);
        return user.Id;

    }
}