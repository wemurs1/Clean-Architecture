namespace Basket.Application.DTOs;

public record CreateShoppingCartItemDto
{
    public required string ProductId { get; set; }
    public required string ProductName { get; set; }
    public required string ImageFile { get; set; }
    public required decimal Price { get; set; }
    public required int Quantity { get; set; }
};
