using Basket.Application.Mappers;
using Basket.Application.Queries;
using Basket.Application.Responses;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Handlers;

public class GetBasketByUserNameQueryHandler(IBasketRepository basketRepository) : IRequestHandler<GetBasketByUserNameQuery, ShoppingCartResponse>
{
    public async Task<ShoppingCartResponse> Handle(GetBasketByUserNameQuery request, CancellationToken cancellationToken)
    {
        var result = await basketRepository.GetBasketAsync(request.UserName);
        if (result == null) return new ShoppingCartResponse(request.UserName);
        return result.ToResponse();
    }
}
