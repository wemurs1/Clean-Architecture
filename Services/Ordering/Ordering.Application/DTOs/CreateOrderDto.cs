namespace Ordering.Application.DTOs;

public record CreateOrderDto(
    int Id,
    string UserName,
    decimal TotalPrice,
    string FirstName,
    string LastName,
    string EmailAddress,
    string AddressLine,
    string Country,
    string State,
    string ZipCode,
    string CardName,
    string CardNumber,
    string Cvv,
    int PaymentMethod
);
