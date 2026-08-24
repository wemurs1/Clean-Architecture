using Basket.Application.Commands;
using Basket.Application.DTOs;
using Basket.Application.Responses;
using Basket.Core.Entities;

namespace Basket.Application.Mappers;

public static class ShoppingCartMapper
{
    public static ShoppingCartResponse ToResponse(this ShoppingCart cart)
    {
        return new ShoppingCartResponse
        {
            UserName = cart.UserName,
            Items = cart.Items.Select(item => item.ToResponse()).ToList()
        };
    }

    public static ShoppingCart ToEntity(this CreateShoppingCartCommand cart)
    {
        return new ShoppingCart(cart.UserName)
        {
            Items = cart.Items.Select(item => item.ToEntity()).ToList()
        };
    }

    public static ShoppingCartDto ToDto(this ShoppingCartResponse response)
    {
        return new ShoppingCartDto(
            UserName: response.UserName,
            Items: response.Items.Select(item => item.ToDto()).ToList(),
            TotalPrice: response.TotalPrice
        );
    }
}
