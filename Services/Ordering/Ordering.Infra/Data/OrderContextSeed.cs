using Microsoft.Extensions.Logging;
using Ordering.Core.Entities;

namespace Ordering.Infra.Data;

public class OrderContextSeed
{
    public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
    {
        if (!orderContext.Orders.Any())
        {
            orderContext.Orders.AddRange(GetOrders());
            await orderContext.SaveChangesAsync();
            logger.LogInformation("Ordering Database {DBName} seeded", typeof(OrderContext).Name);
        }
    }

    private static IEnumerable<Order> GetOrders()
    {
        return new List<Order>
        {
            new() {
                UserName = "fred",
                FirstName = "Fred",
                LastName = "Jones",
                BusinessEmail = "fred.jones@ecommerce.net",
                AddressLine = "10 Downing Street",
                Country = "United Kingdom",
                City = "London",
                State = "England",
                ZipCode = "SW1A 2AA",

                CardName = "Visa",
                CardNumber = "4242424242424242",
                Expiration = "1230",
                Cvv = "123",
                CreatedBy = "fred",
                PaymentMethod = 1,

                LastModifiedBy = "fred",
                LastModifiedDate = DateTime.UtcNow
            }
        };
    }
}
