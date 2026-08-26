using Discount.Application.Commands;
using Discount.Application.Mappers;
using Discount.Application.Queries;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;

namespace Discount.API.Services;

public class DiscountService(IMediator mediator, ILogger<DiscountService> logger) : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        logger.LogInformation($"starting with '{request.ProductName}'");
        var query = new GetDiscountQuery(request.ProductName);
        var dto = await mediator.Send(query);
        if (dto == null) return null!;
        return dto.ToModel();
    }

    public async override Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var cmd = request.Coupon.ToCreateCommand();
        var dto = await mediator.Send(cmd);
        return dto.ToModel();
    }

    public async override Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var cmd = request.Coupon.ToUpdateCommand();
        var dto = await mediator.Send(cmd);
        return dto.ToModel();
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var cmd = new DeleteDiscountCommand(request.ProductName);
        var deleted = await mediator.Send(cmd);
        return new DeleteDiscountResponse
        {
            Success = deleted
        };
    }
}
