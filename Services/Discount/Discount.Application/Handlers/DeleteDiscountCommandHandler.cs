using Discount.Application.Commands;
using Discount.Application.Extensions;
using Discount.Core.Repositories;
using MediatR;

namespace Discount.Application.Handlers;

public class DeleteDiscountCommandHandler(IDiscountRepository discountRepository) : IRequestHandler<DeleteDiscountCommand, bool>
{
    public async Task<bool> Handle(DeleteDiscountCommand command, CancellationToken cancellationToken)
    {
        var validationErrors = new Dictionary<string, string>();
        if (string.IsNullOrWhiteSpace(command.ProductName)) validationErrors["ProductName"] = "Product name must not be empty";
        if (validationErrors.Any()) throw GrpcErrorHelper.CreateValidationException(validationErrors);

        var deleted = await discountRepository.DeleteDiscountAsync(command.ProductName);
        return deleted;
    }
}
