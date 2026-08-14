using Catalog.Application.Commands;
using Catalog.Application.Mappers;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers;

public class CreateProductCommandHandler(IProductRepository productRepository) : IRequestHandler<CreateProductCommand, ProductResponse>
{
    public async Task<ProductResponse> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var brand = await productRepository.GetBrandsByIdAsync(command.Dto.BrandId);
        var type = await productRepository.GetTypesByIdAsync(command.Dto.TypeId);

        if (brand == null || type == null) throw new ApplicationException("Invalid Brand or Type specified");

        var productEntity = command.Dto.ToEntity(brand, type);
        var newProduct = await productRepository.CreateAsync(productEntity);
        return newProduct.ToResponse();
    }
}
