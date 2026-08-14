using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Specifications;

namespace Catalog.Application.Mappers;

public static class ProductMapper
{
    public static ProductResponse ToResponse(this Product product)
    {
        if (product == null) return null!;
        return new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Summary = product.Summary,
            Decription = product.Decription,
            ImageFile = product.ImageFile,
            Brand = product.Brand.ToResponse(),
            Type = product.Type.ToResponse(),
            CreatedDate = product.CreatedDate,
            Price = product.Price
        };
    }

    public static Pagination<ProductResponse> ToResponseList(this Pagination<Product> pagination)
        => new Pagination<ProductResponse>(
            pagination.PageIndex,
            pagination.PageSize,
            pagination.Count,
            pagination.Data.Select(x => x.ToResponse()).ToList()
        );
}
