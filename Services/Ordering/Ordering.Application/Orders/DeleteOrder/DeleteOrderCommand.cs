using Ordering.Application.Abstractions;

namespace Ordering.Application;

public record DeleteOrderCommand(int Id) : ICommand;
