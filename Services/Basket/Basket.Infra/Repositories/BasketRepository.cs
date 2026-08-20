using System.Text.Json;
using Basket.Core.Entities;
using Basket.Core.Repositories;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.Infra.Repositories;

public class BasketRepository(IDistributedCache redisCache) : IBasketRepository
{
    public async Task DeleteBasketAsync(string userName)
    {
        await redisCache.RemoveAsync(userName);
    }

    public async Task<ShoppingCart?> GetBasketAsync(string userName)
    {
        var basket = await redisCache.GetStringAsync(userName);
        if (string.IsNullOrEmpty(basket)) return null;
        return JsonSerializer.Deserialize<ShoppingCart>(basket);
    }

    public async Task<ShoppingCart> UpsertBasketAsync(ShoppingCart shoppingCart)
    {
        await redisCache.SetStringAsync(shoppingCart.UserName, JsonSerializer.Serialize(shoppingCart));
        return (await GetBasketAsync(shoppingCart.UserName))!;
    }
}
