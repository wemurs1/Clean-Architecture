namespace Basket.Application.Responses;

public record ShoppingCartItemResponse
{
    public required int Quantity { get; init; }
    public required string ImageFile { get; init; }
    public required decimal Price { get; init; }
    public required string ProductId { get; init; }
    public required string ProductName { get; init; }
}