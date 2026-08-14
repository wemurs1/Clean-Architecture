using Catalog.Application.Commands;
using Catalog.Application.Mappers;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers;

public class UpdateProductCommandHandler(IProductRepository productRepository) : IRequestHandler<UpdateProductCommand, bool>
{
    public async Task<bool> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var existing = await productRepository.GetByIdASync(command.Dto.Id) ?? throw new KeyNotFoundException($"Product with id {command.Dto.Id} not found");

        var brand = await productRepository.GetBrandsByIdAsync(command.Dto.BrandId);
        var type = await productRepository.GetTypesByIdAsync(command.Dto.TypeId);
        if (brand == null || type == null) throw new ApplicationException("Invalid Brand of type specificed");

        var updatedProduct = command.Dto.ToUpdatedEntity(existing, brand, type);
        return await productRepository.UpdateAsync(updatedProduct);
    }
}
