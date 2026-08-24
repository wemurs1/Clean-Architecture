using Basket.Application.Commands;
using Basket.Application.DTOs;
using Basket.Application.Mappers;
using Basket.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class BasketController(IMediator mediator) : ControllerBase
{

    [HttpGet("{userName}")]
    public async Task<ActionResult<ShoppingCartDto>> GetBasketAsync(string userName)
    {
        var query = new GetBasketByUserNameQuery(userName);
        var shoppingCartResponse = await mediator.Send(query);
        if (shoppingCartResponse == null) return NotFound();
        return shoppingCartResponse.ToDto();
    }

    [HttpPost]
    public async Task<ActionResult<ShoppingCartDto>> CreateOrUpdateBasketAsync(CreateShoppingCartCommand command)
    {
        var cart = await mediator.Send(command);
        if (cart == null) return NotFound();
        return cart.ToDto();
    }

    [HttpDelete("{userName}")]
    public async Task<ActionResult> DeleteBasketAsync(string userName)
    {
        await mediator.Send(new DeleteBasketByUserNameCommand(userName));
        return NoContent();
    }
}
