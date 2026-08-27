using Discount.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Discount.Infra.Data;

public class DiscountDbContext(DbContextOptions<DiscountDbContext> options) : DbContext(options)
{
    public DbSet<Coupon> Coupons { get; set; }
}
