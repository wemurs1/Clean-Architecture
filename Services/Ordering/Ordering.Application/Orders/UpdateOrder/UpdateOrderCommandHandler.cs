using Microsoft.Extensions.Logging;
using Ordering.Application.Abstractions;
using Ordering.Application.Exceptions;
using Ordering.Application.Mapper;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;

namespace Ordering.Application.Orders.UpdateOrder;

public class UpdateOrderCommandHandler(IOrderRepository orderRepository, ILogger<UpdateOrderCommandHandler> logger) : ICommandHandler<UpdateOrderCommand>
{
    public async Task Handle(UpdateOrderCommand command)
    {
        var orderToUpdate = await orderRepository.GetByIdAsync(command.Id);
        if (orderToUpdate == null)
        {
            throw new OrderNotFoundException(nameof(Order), command.Id);
        }
        orderToUpdate.ApplyUpdate(command);
        await orderRepository.UpdateAsync(orderToUpdate);
        logger.LogInformation("Order with ID: {id} has been updated", command.Id);
    }
}
