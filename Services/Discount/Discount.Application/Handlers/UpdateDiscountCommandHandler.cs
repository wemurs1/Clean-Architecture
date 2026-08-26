using Discount.Application.Commands;
using Discount.Application.DTOs;
using Discount.Application.Extensions;
using Discount.Application.Mappers;
using Discount.Core.Repositories;
using Grpc.Core;
using MediatR;

namespace Discount.Application.Handlers;

public class UpdateDiscountCommandHandler(IDiscountRepository discountRepository) : IRequestHandler<UpdateDiscountCommand, CouponDto>
{
    public async Task<CouponDto> Handle(UpdateDiscountCommand command, CancellationToken cancellationToken)
    {
        var validationErrors = new Dictionary<string, string>();
        if (string.IsNullOrWhiteSpace(command.ProductName)) validationErrors["ProductName"] = "Product name must not be empty";
        if (string.IsNullOrWhiteSpace(command.Description)) validationErrors["Description"] = "Product Description mus not be empty";
        if (command.Amount <= 0) validationErrors["Amount"] = "Amount must be greater than 0";
        if (validationErrors.Any()) throw GrpcErrorHelper.CreateValidationException(validationErrors);

        var coupon = command.ToEntity();
        var updated = await discountRepository.UpdateDiscountAsync(coupon);
        if (!updated) throw new RpcException(new Status(StatusCode.Internal, $"Could not update discount for product {command.ProductName}"));

        return coupon.ToDto();
    }
}
