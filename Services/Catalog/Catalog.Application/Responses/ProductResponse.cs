namespace Catalog.Application.Responses;

public record ProductResponse
{
    public required string Id { get; set; }
    public required string Name { get; init; }
    public required string Summary { get; init; }
    public required string Decription { get; init; }
    public required string ImageFile { get; init; }
    public required BrandResponse Brand { get; init; }
    public required TypeResponse Type { get; init; }
    public decimal Price { get; init; }
    public DateTimeOffset CreatedDate { get; init; }
}
