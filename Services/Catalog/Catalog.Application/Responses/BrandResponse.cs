namespace Catalog.Application.Responses;

public record BrandResponse
{
    public required string Id { get; init; }
    public required string Name { get; init; }
}
