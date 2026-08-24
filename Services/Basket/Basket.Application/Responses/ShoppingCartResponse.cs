namespace Basket.Application.Responses;

public record ShoppingCartResponse
{
    public string UserName { get; init; }
    public List<ShoppingCartItemResponse> Items { get; init; }

    public ShoppingCartResponse()
    {
        UserName = string.Empty;
        Items = [];
    }

    public ShoppingCartResponse(string userName, List<ShoppingCartItemResponse> items)
    {
        UserName = userName;
        Items = items;
    }

    public ShoppingCartResponse(string userName) : this(userName, []) { }

    public decimal TotalPrice => Items.Sum(item => item.Price * item.Quantity);
}
