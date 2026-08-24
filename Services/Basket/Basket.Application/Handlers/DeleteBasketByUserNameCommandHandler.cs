using Basket.Application.Commands;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Handlers;

public class DeleteBasketByUserNameCommandHandler(IBasketRepository basketRepository) : IRequestHandler<DeleteBasketByUserNameCommand, Unit>
{
    public async Task<Unit> Handle(DeleteBasketByUserNameCommand command, CancellationToken cancellationToken)
    {
        await basketRepository.DeleteBasketAsync(command.UserName);
        return Unit.Value;
    }
}
