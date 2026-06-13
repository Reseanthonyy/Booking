using Booking.Application.Auth.Commands.Login;
using Booking.Application.Auth.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<ActionResult<Guid>> Register([FromBody] RegisterUserCommand command)
    {
        var userId = await _mediator.Send(command);
        return Ok(userId);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<Guid>> Login([FromBody] LoginUserCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}