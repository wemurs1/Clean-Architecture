using Microsoft.Extensions.Logging;
using Ordering.Application.Abstractions;
using Ordering.Application.Exceptions;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;

namespace Ordering.Application.Orders.DeleteOrder;

public class DeleteOrderCommandHandler(IOrderRepository orderRepository, ILogger<DeleteOrderCommandHandler> logger)
: ICommandHandler<DeleteOrderCommand>
{
    public async Task Handle(DeleteOrderCommand command)
    {
        var orderToDelete = await orderRepository.GetByIdAsync(command.Id);
        if (orderToDelete == null)
        {
            throw new OrderNotFoundException(nameof(Order), command.Id);
        }

        await orderRepository.DeleteAsync(orderToDelete);
        logger.LogInformation("Order with ID: {id} has been deleted", command.Id);
    }
}
