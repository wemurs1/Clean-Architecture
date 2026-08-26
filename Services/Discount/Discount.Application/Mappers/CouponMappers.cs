using Discount.Application.Commands;
using Discount.Application.DTOs;
using Discount.Core.Entities;
using Discount.Grpc.Protos;

namespace Discount.Application.Mappers;

public static class CouponMappers
{
    public static CouponDto ToDto(this Coupon coupon)
    {
        return new CouponDto(
            Id: coupon.Id,
            ProductName: coupon.ProductName,
            Description: coupon.Description!,
            Amount: coupon.Amount
        );
    }

    public static Coupon ToEntity(this CreateDiscountCommand command)
    {
        return new Coupon
        {
            ProductName = command.ProductName,
            Description = command.Description,
            Amount = command.Amount
        };
    }

    public static Coupon ToEntity(this UpdateDiscountCommand command)
    {
        return new Coupon
        {
            Id = command.Id,
            ProductName = command.ProductName,
            Description = command.Description,
            Amount = command.Amount
        };
    }

    public static CouponModel ToModel(this CouponDto dto)
    {
        return new CouponModel
        {
            Id = dto.Id,
            ProductName = dto.ProductName,
            Description = dto.Description,
            Amount = dto.Amount
        };
    }

    public static CreateDiscountCommand ToCreateCommand(this CouponModel request)
    {
        return new CreateDiscountCommand(
            ProductName: request.ProductName,
            Description: request.Description,
            Amount: request.Amount
        );
    }

    public static UpdateDiscountCommand ToUpdateCommand(this CouponModel request)
    {
        return new UpdateDiscountCommand(
            Id: request.Id,
            ProductName: request.ProductName,
            Description: request.Description,
            Amount: request.Amount
        );
    }
}
