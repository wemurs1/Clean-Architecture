using Discount.Core.Entities;
using Discount.Core.Repositories;
using Discount.Infra.Data;

namespace Discount.Infra.Repositories;

public class DiscountRepository(DiscountDbContext context) : IDiscountRepository
{
    public async Task<bool> CreateDiscountAsync(Coupon coupon)
    {
        context.Add(coupon);
        var affected = await context.SaveChangesAsync();
        return affected > 0;
    }

    public async Task<bool> DeleteDiscountAsync(string productName)
    {
        var coupon = await context.Coupons.FindAsync(productName);
        if (coupon == null) return false;
        context.Coupons.Remove(coupon);
        var affected = await context.SaveChangesAsync();
        return affected > 0;
    }

    public async Task<Coupon> GetDiscountAsync(string productName)
    {
        var coupon = await context.Coupons.FindAsync(productName);
        return coupon ?? new Coupon
        {
            ProductName = "No Discount",
            Amount = 0,
            Description = "No Discount Available"
        };
    }

    public async Task<bool> UpdateDiscountAsync(Coupon coupon)
    {
        context.Coupons.Update(coupon);
        var affected = await context.SaveChangesAsync();
        return affected > 0;
    }
}
