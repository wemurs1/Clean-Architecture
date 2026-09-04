using Ordering.Application.Abstractions;
using Ordering.Application.DTOs;
using Ordering.Application.Mapper;
using Ordering.Core.Repositories;

namespace Ordering.Application.Orders.GetOrders;

public class GetOrderListQueryHandler(IOrderRepository orderRepository) : IQueryHandler<GetOrderListQuery, List<OrderDto>>
{
    public async Task<List<OrderDto>> Handle(GetOrderListQuery query, CancellationToken cancellationToken)
    {
        var orders = await orderRepository.GetOrderByUserNameAsync(query.UserName);
        return orders.Select(o => o.ToDto()).ToList();
    }
}
