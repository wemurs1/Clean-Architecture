namespace Basket.Core.Entities;

public class BasketCheckout
{
    public required string UserName { get; set; }
    public decimal TotalPrice { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string EamilAddress { get; set; }
    public required string AddressLine { get; set; }
    public required string Country { get; set; }
    public required string State { get; set; }
    public required string ZipCode { get; set; }
    public required string CardName { get; set; }
    public required string CardNumber { get; set; }
    public required string Expiration { get; set; }
    public required string Cvv { get; set; }
    public required string PaymentMethod { get; set; }
}
