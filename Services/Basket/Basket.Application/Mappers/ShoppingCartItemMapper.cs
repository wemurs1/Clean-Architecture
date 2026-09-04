using Basket.Application.DTOs;
using Basket.Application.Responses;
using Basket.Core.Entities;
using EventBus.Messages.Events;

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

    public static ShoppingCartItem ToEntity(this ShoppingCartItemResponse item)
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

    public static BasketCheckoutEvent ToBasketCheckoutEvent(this BasketCheckoutDto dto, ShoppingCart basket)
    {
        return new BasketCheckoutEvent
        {
            UserName = dto.UserName,
            TotalPrice = basket.Items.Sum(item => item.Price * item.Quantity),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            EmailAddress = dto.EmailAddress,
            AddressLine = dto.AddressLine,
            Country = dto.Country,
            State = dto.State,
            ZipCode = dto.ZipCode,
            CardName = dto.CardName,
            CardNumber = dto.CardNumber,
            Expiration = dto.Expiration,
            Cvv = dto.Cvv,
            PaymentMethod = dto.PaymentMethod
        };
    }
}
