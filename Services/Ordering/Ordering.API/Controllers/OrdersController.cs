using Microsoft.AspNetCore.Mvc;
using Ordering.Application;
using Ordering.Application.DTOs;
using Ordering.Application.Mapper;
using Ordering.Application.Orders.CreateOrder;
using Ordering.Application.Orders.DeleteOrder;
using Ordering.Application.Orders.GetOrders;
using Ordering.Application.Orders.UpdateOrder;

namespace Ordering.API.Controllers;

[ApiController]
[Route("api/va/[controller]")]
public class OrdersController(
    CreateOrderCommandHandler createOrderCommandHandler,
    UpdateOrderCommandHandler updateOrderCommandHandler,
    DeleteOrderCommandHandler deleteOrderCommandHandler,
    GetOrderListQueryHandler getOrderListQueryHandler,
    ILogger<OrdersController> logger
) : ControllerBase
{
    [HttpGet("{userName}", Name = "GetOrdersByUserName")]
    public async Task<ActionResult<List<OrderDto>>> GetOrdersByUsername(string userName, CancellationToken cancellationToken)
    {
        var query = new GetOrderListQuery(userName);

        var orders = await getOrderListQueryHandler.Handle(query, cancellationToken);

        logger.LogInformation("Orders fetched for user {UserName}", userName);
        return orders;
    }

    [HttpPost(Name = "CheckoutOrder")]
    public async Task<ActionResult<int>> CheckoutOrder(CreateOrderDto dto, CancellationToken cancellationToken)
    {
        var command = dto.ToCommand();

        var orderId = await createOrderCommandHandler.Handle(command, cancellationToken);

        logger.LogInformation("Order created with Id {ID}", orderId);
        return orderId;
    }

    [HttpPut(Name = "UpdateOrder")]
    public async Task<ActionResult> UpdateOrder(OrderDto dto, CancellationToken cancellationToken)
    {
        var command = dto.ToCommand();

        await updateOrderCommandHandler.Handle(command);

        logger.LogInformation("Order updated with Id {ID}", dto.Id);
        return NoContent();
    }

    [HttpDelete("{id}", Name = "DeleteOrder")]
    public async Task<ActionResult> DeleteOrder(int id, CancellationToken cancellationToken)
    {
        var command = new DeleteOrderCommand(id);

        await deleteOrderCommandHandler.Handle(command);

        logger.LogInformation("Order deleted with Id {ID}", id);
        return NoContent();
    }
}
