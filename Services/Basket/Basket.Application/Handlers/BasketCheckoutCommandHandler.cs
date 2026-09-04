using Basket.Application.Commands;
using Basket.Application.Mappers;
using Basket.Application.Queries;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Basket.Application.Handlers;

public class BasketCheckoutCommandHandler(
    IMediator mediator,
    IPublishEndpoint publishEndpoint,
    ILogger<BasketCheckoutCommandHandler> logger
) : IRequestHandler<BasketCheckoutCommand, Unit>
{
    public async Task<Unit> Handle(BasketCheckoutCommand command, CancellationToken cancellationToken)
    {
        var basketDto = command.BasketCheckoutDto;

        var basketResponse = await mediator.Send(new GetBasketByUserNameQuery(basketDto.UserName), cancellationToken);
        if (basketResponse is null || !basketResponse.Items.Any())
        {
            throw new InvalidOperationException("Basket not found or empty");
        }

        var basket = basketResponse.ToEntity();

        var evt = basketDto.ToBasketCheckoutEvent(basket);

        logger.LogInformation("Publishing BasketCheckoutEvent for {user}", basket.UserName);
        await publishEndpoint.Publish(evt, cancellationToken);

        await mediator.Send(new DeleteBasketByUserNameCommand(basketDto.UserName), cancellationToken);
        return Unit.Value;
    }
}
