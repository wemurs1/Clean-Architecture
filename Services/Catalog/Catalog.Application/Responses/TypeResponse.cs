namespace Catalog.Application.Responses;

public record TypeResponse
{
    public required string Id { get; init; }
    public required string Name { get; init; }
}
