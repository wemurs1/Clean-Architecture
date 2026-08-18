using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers;

public class GetProductByNameHandler(IProductRepository productRepository) : IRequestHandler<GetProductByNameQuery, IEnumerable<ProductResponse>>
{
    public async Task<IEnumerable<ProductResponse>> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetProductsByName(request.ProductName);
        return product.ToResponseList();
    }
}
