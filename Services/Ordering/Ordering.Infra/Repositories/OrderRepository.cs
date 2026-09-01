using Microsoft.EntityFrameworkCore;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;
using Ordering.Infra.Data;

namespace Ordering.Infra.Repositories;

public class OrderRepository(OrderContext orderContext) : RepositoryBase<Order>(orderContext), IOrderRepository
{
    private readonly OrderContext _orderContext = orderContext;

    public async Task<IEnumerable<Order>> GetOrderByUserNameAsync(string userName)
    {
        return await _orderContext.Orders.Where(x => x.UserName == userName).AsNoTracking().ToListAsync();
    }
}
