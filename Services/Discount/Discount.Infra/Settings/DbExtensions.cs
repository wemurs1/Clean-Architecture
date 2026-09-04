using Discount.Core.Entities;
using Discount.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Discount.Infra.Settings;

public static class DbExtensions
{
    public static async Task<IHost> MigrateDatabase(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<IHost>>();
        var context = services.GetRequiredService<DiscountDbContext>();
        try
        {
            logger.LogInformation("Discount Db Migration Started");
            await ApplyMigration(context, logger);
            logger.LogInformation("Discount Db Migration Completed");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while migrating the database");
            throw;
        }
        return host;
    }

    private static async Task ApplyMigration(DiscountDbContext context, ILogger<IHost> logger)
    {
        var retry = 5;
        while (retry > 0)
        {
            try
            {
                logger.LogInformation("Executing MigrateASync");
                await context.Database.MigrateAsync();
                logger.LogInformation("Executed MigrateASync");

                if (!context.Coupons.Any())
                {
                    var coupons = new Coupon[]
                    {
                        new() {
                            ProductName="Adidas FIFA World Cup 2018 OMB Football",
                            Description="Football Discount",
                            Amount=500
                        },
                        new() {
                            ProductName="Yonex VCORE Pro 100 A Tennis Racquet (270gm, Strung)",
                            Description="Raquet Discount",
                            Amount=700
                        },
                    };
                    context.Coupons.AddRange(coupons);
                    await context.SaveChangesAsync();
                }
                break;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                logger.LogError("Retry {retry}", retry);
                retry--;
                if (retry == 0) throw;
            }
        }
    }
}
