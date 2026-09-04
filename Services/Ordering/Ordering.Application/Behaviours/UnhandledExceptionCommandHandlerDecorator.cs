using Microsoft.Extensions.Logging;
using Ordering.Application.Abstractions;

namespace Ordering.Application.Behaviours;

public class UnhandledExceptionCommandHandlerDecorator<TCommand, TResult>(ICommandHandler<TCommand, TResult> inner, ILogger<TCommand> logger)
    : ICommandHandler<TCommand, TResult> where TCommand : ICommand<TResult>
{
    public async Task<TResult> Handle(TCommand command, CancellationToken cancellationToken)
    {
        try
        {
            return await inner.Handle(command, cancellationToken);
        }
        catch (Exception ex)
        {
            var commandName = typeof(TCommand).Name;
            logger.LogError(ex, "Unhandled exception occurred in {CommandName}. Command {@Command}", commandName, command);
            throw;
        }
    }
}
