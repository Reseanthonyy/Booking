using Booking.Application.Reservations.Commands.Confirm;
using Booking.Application.Reservations.Commands.Create;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReservationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateReservationCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    [HttpPut("{id:guid}/confirm")]
    [Authorize(Roles = "Admin, Employee")]
    public async Task<IActionResult> Confirm(Guid id)
    {
        await _mediator.Send(new ConfirmReservationCommand(id));
        return NoContent();
    }
}