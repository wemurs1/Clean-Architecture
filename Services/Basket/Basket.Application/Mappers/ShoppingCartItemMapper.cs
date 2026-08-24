using Basket.Application.DTOs;
using Basket.Application.Responses;
using Basket.Core.Entities;

namespace Basket.Application.Mappers;

public static class ShoppingCartItemMapper
{
    public static ShoppingCartItemResponse ToResponse(this ShoppingCartItem item)
    {
        return new ShoppingCartItemResponse
        {
            Quantity = item.Quantity,
            ImageFile = item.ImageFile,
            Price = item.Price,
            ProductId = item.ProductId,
            ProductName = item.ProductName
        };
    }

    public static ShoppingCartItem ToEntity(this CreateShoppingCartItemDto item)
    {
        return new ShoppingCartItem
        {
            Quantity = item.Quantity,
            Price = item.Price,
            ProductId = item.ProductId,
            ProductName = item.ProductName,
            ImageFile = item.ImageFile
        };
    }

    public static ShoppingCartItemDto ToDto(this ShoppingCartItemResponse item)
    {
        return new ShoppingCartItemDto(
            ProductId: item.ProductId,
            ProductName: item.ProductName,
            ImageFile: item.ImageFile,
            Price: item.Price,
            Quantity: item.Quantity
        );
    }
}
