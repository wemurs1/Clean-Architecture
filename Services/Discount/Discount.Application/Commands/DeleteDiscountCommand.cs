using MediatR;

namespace Discount.Application.Commands;

public record class DeleteDiscountCommand(string ProductName) : IRequest<bool>;
