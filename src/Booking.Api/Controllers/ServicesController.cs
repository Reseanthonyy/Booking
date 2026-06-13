using Booking.Application.Services.Commands.Create;
using Booking.Application.Services.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class ServicesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ServicesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetServices(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] bool? isActive = null)
    {
        var result = await _mediator.Send(new GetServicesQuery(pageNumber, pageSize, isActive));
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateServiceCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetServices), new { id }, id);
    }
    
    
}