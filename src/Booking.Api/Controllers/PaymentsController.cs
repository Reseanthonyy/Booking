using Booking.Application.Payments.Commands.MarkAsPaid;
using Booking.Application.Payments.Commands.Register;
using Booking.Application.Payments.Queries.GetOverdue;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PaymentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Register([FromBody] RegisterPaymentCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    [HttpPut("{id:guid}/pay")]
    [Authorize(Roles = "Admin,Employee")]
    public async Task<IActionResult> MarkAsPaid(Guid id, [FromBody] MarkPaymentAsPaidCommand command)
    {
        if (id != command.Id)
            return BadRequest("Id mismatch");
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet("overdue")]
    public async Task<IActionResult> GetOverdue(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 20)
    {
        var result = await _mediator.Send(new GetOverduePaymentsQuery(pageNumber, pageSize));
        return Ok(result);
    }
}