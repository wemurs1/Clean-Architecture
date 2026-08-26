using Basket.Application.Commands;
using Basket.Application.GrpcService;
using Basket.Application.Mappers;
using Basket.Application.Responses;
using Basket.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Basket.Application.Handlers;

public class CreateShoppingCartCommandHandler(
    IBasketRepository basketRepository,
    DiscountGrpcService discountGrpcService,
    ILogger<CreateShoppingCartCommandHandler> logger
) : IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
{
    public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand command, CancellationToken cancellationToken)
    {
        foreach (var item in command.Items)
        {
            var coupon = await discountGrpcService.GetDiscountAsync(item.ProductName);
            logger.LogInformation($"{coupon.ProductName}: {coupon.Description} - {coupon.Amount}");
            item.Price -= coupon.Amount;
        }
        var shoppingCartEntity = command.ToEntity();
        var result = await basketRepository.UpsertBasketAsync(shoppingCartEntity);
        if (result == null) return null!;
        return shoppingCartEntity.ToResponse();
    }
}