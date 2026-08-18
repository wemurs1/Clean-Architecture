using Catalog.Application.DTOs;
using Catalog.Application.Responses;
using Catalog.Core.Entities;

namespace Catalog.Application.Mappers;

public static class BrandMapper
{
    public static BrandResponse ToResponse(this ProductBrand brand)
    {
        return new BrandResponse
        {
            Id = brand.Id,
            Name = brand.Name
        };
    }

    public static IEnumerable<BrandResponse> ToResponseList(this IEnumerable<ProductBrand> brands)
        => brands.Select(x => x.ToResponse()).ToList();

    public static BrandDto ToDto(this BrandResponse response)
        => new BrandDto(
            Id: response.Id,
            Name: response.Name
        );
}
