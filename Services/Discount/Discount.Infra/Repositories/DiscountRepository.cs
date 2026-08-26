using Dapper;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Discount.Infra.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Discount.Infra.Repositories;

public class DiscountRepository(IOptions<DatabaseSettings> databaseSettings, ILogger<DiscountRepository> logger) : IDiscountRepository
{
    private readonly string _connectionString = databaseSettings.Value.ConnectionString;

    public async Task<bool> CreateDiscountAsync(Coupon coupon)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        var affected = await connection.ExecuteAsync(
            "INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)",
            new { coupon.ProductName, coupon.Description, coupon.Amount }
        );
        return affected > 0;
    }

    public async Task<bool> DeleteDiscountAsync(string productName)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        var affected = await connection.ExecuteAsync(
            "DELETE FROM Coupon WHERE ProductName = @ProductName",
            new { ProductName = productName }
        );
        return affected > 0;
    }

    public async Task<Coupon> GetDiscountAsync(string productName)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>(
            "SELECT * FROM Coupon WHERE ProductName = @ProductName",
            new { ProductName = productName }
        );
        var message = productName + coupon == null ? ": not found" : ": found";
        logger.LogInformation(message);
        return coupon ?? new Coupon
        {
            ProductName = "No Discount",
            Amount = 0,
            Description = "No Discount Available"
        };
    }

    public async Task<bool> UpdateDiscountAsync(Coupon coupon)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        var affected = await connection.ExecuteAsync(
            "UPDATE Coupon SET ProductName = @ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id",
            new { coupon.ProductName, coupon.Description, coupon.Amount, coupon.Id }
        );
        return affected > 0;
    }
}
