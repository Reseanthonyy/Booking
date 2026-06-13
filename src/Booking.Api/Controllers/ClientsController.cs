using Booking.Application.Clients.Commands.Create;
using Booking.Application.Clients.Commands.Delete;
using Booking.Application.Clients.Commands.Update;
using Booking.Application.Clients.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ClientsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetClients(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] string? searchTerm = null)
    {
        var result = await _mediator.Send(new GetClientsQuery(pageNumber, pageSize, searchTerm));
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var client = await _mediator.Send(new GetClientByIdQuery(id));
        return client is null ? NotFound() : Ok(client);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateClientCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateClientCommand command)
    {
        if(id != command.Id)
            return BadRequest("Id mismatch");
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteClientCommand(id));
        return NoContent();
    }
}