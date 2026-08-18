using Catalog.Application.DTOs;
using Catalog.Application.Responses;
using Catalog.Core.Entities;

namespace Catalog.Application.Mappers;

public static class TypeMapper
{
    public static TypeResponse ToResponse(this ProductType type)
    {
        return new TypeResponse
        {
            Id = type.Id,
            Name = type.Name
        };
    }

    public static IEnumerable<TypeResponse> ToResponseList(this IEnumerable<ProductType> types)
        => types.Select(x => x.ToResponse()).ToList();

    public static TypeDto ToDto(this TypeResponse response)
        => new TypeDto(
            Id: response.Id,
            Name: response.Name
        );
}
