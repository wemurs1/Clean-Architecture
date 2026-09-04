using FluentValidation;
using Ordering.Application.Abstractions;

namespace Ordering.Application.Behaviours;

public class ValidationCommandHandlerDecorator<TCommand, TResult>(ICommandHandler<TCommand, TResult> inner, IEnumerable<IValidator<TCommand>> validators)
    : ICommandHandler<TCommand, TResult> where TCommand : ICommand<TResult>
{
    public async Task<TResult> Handle(TCommand command, CancellationToken cancellationToken)
    {
        if (validators.Any())
        {
            var context = new ValidationContext<TCommand>(command);
            var results = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            var failures = results.SelectMany(r => r.Errors).Where(f => f != null).ToList();
            if (failures.Any()) throw new ValidationException(failures);
        }
        return await inner.Handle(command, cancellationToken);
    }
}
