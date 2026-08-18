using Catalog.Application.Commands;
using Catalog.Application.DTOs;
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

    public static IEnumerable<ProductResponse> ToResponseList(this IEnumerable<Product> products)
        => products.Select(x => x.ToResponse()).ToList();

    public static Product ToEntity(this CreateProductDto dto, ProductBrand brand, ProductType type)
        => new Product
        {
            Name = dto.Name,
            Summary = dto.Summary,
            Decription = dto.Description,
            ImageFile = dto.ImageFile,
            Brand = brand,
            Type = type,
            CreatedDate = DateTime.UtcNow,
            Price = dto.Price
        };


    public static Product ToUpdatedEntity(this UpdateProductDto dto, Product existing, ProductBrand brand, ProductType type)
        => new Product
        {
            Id = existing.Id,
            Name = dto.Name,
            Summary = dto.Summary,
            Decription = dto.Description,
            ImageFile = dto.ImageFile,
            Brand = brand,
            Type = type,
            CreatedDate = existing.CreatedDate,
            Price = dto.Price
        };

    public static ProductDto ToDto(this ProductResponse response)
        => new ProductDto
        (
            Id: response.Id,
            Name: response.Name,
            Summary: response.Summary,
            Description: response.Decription,
            ImageFile: response.ImageFile,
            Brand: new BrandDto(response.Brand.Id, response.Brand.Name),
            Type: new TypeDto(response.Type.Id, response.Type.Name),
            CreatedDate: response.CreatedDate,
            Price: response.Price
        );

    public static Pagination<ProductDto> ToPaginatedDto(this Pagination<ProductResponse> response)
        => new Pagination<ProductDto>
        (
            pageIndex: response.PageIndex,
            pageSize: response.PageSize,
            count: response.Count,
            data: response.Data.Select(p => p.ToDto()).ToList()
        );

    public static UpdateProductCommand ToCommand(this UpdateProductDto dto, string productId)
    {
        dto.Id = productId;
        return new UpdateProductCommand(dto);
    }
}
