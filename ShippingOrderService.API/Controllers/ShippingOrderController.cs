using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShippingOrderService.Application.Commands.AddShippingOrderItem;
using ShippingOrderService.Application.Commands.ChangeShippingOrderState;
using ShippingOrderService.Application.Commands.CreateShippingOrder;
using ShippingOrderService.Application.Commands.RemoveShippingOrderItem;
using ShippingOrderService.Application.Queries.GetShippingOrderById;

namespace SHOService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShippingOrderController : ControllerBase
{
    private readonly IMediator _mediator;

    public ShippingOrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // 1. Create Shipping Order
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateShippingOrderCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, new { Id = id });
    }

    // 2. Add Item to Shipping Order
    [HttpPost("{id}/items")]
    public async Task<IActionResult> AddItem(int id, [FromBody] AddShippingOrderItemCommand command)
    {
        if (id != command.ShippingOrderId)
            return BadRequest("ShippingOrderId in URL does not match ShippingOrderId in body.");

        await _mediator.Send(command);
        return NoContent();
    }

    // 3. Change Shipping Order State
    [HttpPut("{id}/state")]
    public async Task<IActionResult> ChangeState(int id, [FromBody] ChangeShippingOrderStateCommand command)
    {
        if (id != command.ShippingOrderId)
            return BadRequest("ShippingOrderId in URL does not match ShippingOrderId in body.");

        await _mediator.Send(command);
        return NoContent();
    }

    // 4. Remove Item from Shipping Order
    [HttpDelete("{id}/items/{itemId}")]
    public async Task<IActionResult> RemoveItem(int id, int itemId)
    {
        await _mediator.Send(new RemoveShippingOrderItemCommand(id, itemId));
        return NoContent();
    }

    // (Optional) Get Shipping Order by ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var result = await _mediator.Send(new GetShippingOrderByIdQuery(id));
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An error occurred.", Details = ex.Message });
        }
    }

}
