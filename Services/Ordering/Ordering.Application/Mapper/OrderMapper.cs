using Ordering.Application.DTOs;
using Ordering.Application.Orders.CreateOrder;
using Ordering.Core.Entities;

namespace Ordering.Application.Mapper;

public static class OrderMapper
{
    public static OrderDto ToDto(this Order order)
    {
        return new OrderDto(
            Id: order.Id,
            UserName: order.UserName!,
            TotalPrice: order.TotalPrice ?? 0,
            FirstName: order.FirstName!,
            LastName: order.LastName!,
            EmailAddress: order.BusinessEmail!,
            AddressLine: order.AddressLine!,
            Country: order.Country!,
            State: order.State!,
            ZipCode: order.ZipCode!,
            CardName: order.CardName!,
            CardNumber: order.CardNumber!,
            Expiration: order.Expiration!,
            Cvv: order.Cvv!,
            PaymentMethod: order.PaymentMethod ?? 0
        );
    }

    public static Order ToEntity(this CreateOrderCommand command)
    {
        return new Order
        {
            UserName = command.UserName,
            TotalPrice = command.TotalPrice,
            FirstName = command.FirstName,
            LastName = command.LastName,
            BusinessEmail = command.EmailAddress,
            AddressLine = command.AddressLine,
            Country = command.Country,
            State = command.State,
            ZipCode = command.ZipCode,
            CardName = command.CardName,
            CardNumber = command.CardNumber,
            Expiration = command.Expiration,
            Cvv = command.Cvv,
            PaymentMethod = command.PaymentMethod
        };
    }

    public static void ApplyUpdate(this Order orderToUpdate, UpdateOrderCommand command)
    {
        orderToUpdate.UserName = command.UserName;
        orderToUpdate.TotalPrice = command.TotalPrice;
        orderToUpdate.FirstName = command.FirstName;
        orderToUpdate.LastName = command.LastName;
        orderToUpdate.BusinessEmail = command.EmailAddress;
        orderToUpdate.AddressLine = command.AddressLine;
        orderToUpdate.Country = command.Country;
        orderToUpdate.State = command.State;
        orderToUpdate.ZipCode = command.ZipCode;
        orderToUpdate.CardName = command.CardName;
        orderToUpdate.CardNumber = command.CardNumber;
        orderToUpdate.Expiration = command.Expiration;
        orderToUpdate.Cvv = command.Cvv;
        orderToUpdate.PaymentMethod = command.PaymentMethod;
    }

    public static CreateOrderCommand ToCommand(this CreateOrderDto dto)
    {
        return new CreateOrderCommand
        {
            UserName = dto.UserName,
            TotalPrice = dto.TotalPrice,
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

    public static UpdateOrderCommand ToCommand(this OrderDto dto)
    {
        return new UpdateOrderCommand
        {
            Id = dto.Id,
            UserName = dto.UserName,
            TotalPrice = dto.TotalPrice,
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
