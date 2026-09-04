using Ordering.Application.Abstractions;
using Ordering.Application.Mapper;
using Ordering.Core.Repositories;

namespace Ordering.Application.Orders.CreateOrder;

public class CreateOrderCommandHandler(IOrderRepository orderRepository) : ICommandHandler<CreateOrderCommand, int>
{
    public async Task<int> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var orderEntity = command.ToEntity();
        var generatedOrder = await orderRepository.AddAsync(orderEntity);
        return generatedOrder.Id;
    }
}
