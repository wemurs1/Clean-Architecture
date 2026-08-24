using Basket.Application.Commands;
using Basket.Application.Mappers;
using Basket.Application.Responses;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Handlers;

public class CreateShoppingCartCommandHandler(IBasketRepository basketRepository) : IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
{
    public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand command, CancellationToken cancellationToken)
    {
        var shoppingCartEntity = command.ToEntity();
        var result = await basketRepository.UpsertBasketAsync(shoppingCartEntity);
        if (result == null) return null!;
        return shoppingCartEntity.ToResponse();
    }
}