using Discount.Application.DTOs;
using Discount.Application.Extensions;
using Discount.Application.Mappers;
using Discount.Application.Queries;
using Discount.Core.Repositories;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Discount.Application.Handlers;

public class GetDiscountQueryHandler(IDiscountRepository discountRepository, ILogger<GetDiscountQueryHandler> logger)
: IRequestHandler<GetDiscountQuery, CouponDto?>
{
    public async Task<CouponDto?> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.ProductName))
        {
            var validationErrors = new Dictionary<String, string>
            {
                {"ProductName","Product name must not be empty"}
            };
            throw GrpcErrorHelper.CreateValidationException(validationErrors);
        }
        var coupon = await discountRepository.GetDiscountAsync(request.ProductName) ??
            throw new RpcException(new Status(StatusCode.Internal, $"Could not retrieve discount for product: {request.ProductName}"));
        logger.LogInformation($"Handling: {request.ProductName} - amount {coupon.Amount}");
        return coupon.ToDto();
    }
}
