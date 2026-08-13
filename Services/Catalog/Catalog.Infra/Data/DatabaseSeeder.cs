using System.Data;
using System.Text.Json;
using Catalog.Core.Entities;
using Catalog.Infra.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Catalog.Infra.Data;

public class DatabaseSeeder
{
    public static async Task SeedAsync(IOptions<DatabaseSettings> options)
    {
        var settings = options.Value;
        var db = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
        var brands = db.GetCollection<ProductBrand>(settings.BrandCollectionName);
        var types = db.GetCollection<ProductType>(settings.TypeCollectionName);
        var products = db.GetCollection<Product>(settings.ProductCollectionName);

        var seedBasePath = Path.Combine("Data", "SeedData");

        // Seed Brands
        List<ProductBrand> brandlist = [];
        if ((await brands.CountDocumentsAsync(_ => true)) == 0)
        {
            var brandData = await File.ReadAllTextAsync(Path.Combine(seedBasePath, "brands.json")) ?? throw new DataException("No brand seed file");
            brandlist = JsonSerializer.Deserialize<List<ProductBrand>>(brandData) ?? throw new DataException("Brand seed file has json error");
            await brands.InsertManyAsync(brandlist);
        }
        else
        {
            brandlist = await brands.Find(_ => true).ToListAsync();
        }

        // Seed Types
        List<ProductType> typelist = [];
        if ((await types.CountDocumentsAsync(_ => true)) == 0)
        {
            var typeData = await File.ReadAllTextAsync(Path.Combine(seedBasePath, "types.json")) ?? throw new DataException("No type seed file");
            typelist = JsonSerializer.Deserialize<List<ProductType>>(typeData) ?? throw new DataException("Type seed file has json error");
            await types.InsertManyAsync(typelist);
        }
        else
        {
            typelist = await types.Find(_ => true).ToListAsync();
        }

        // Seed Products
        List<Product> productlist = [];
        if ((await products.CountDocumentsAsync(_ => true)) == 0)
        {
            var productData = await File.ReadAllTextAsync(Path.Combine(seedBasePath, "products.json")) ?? throw new DataException("No product seed file");
            productlist = JsonSerializer.Deserialize<List<Product>>(productData) ?? throw new DataException("Product seed file has json error");
            foreach (var product in productlist)
            {
                // Reset id to let Mongo generate one
                product.Id = null!;
                // Default created date if nor set
                if (product.CreatedDate == default) product.CreatedDate = DateTime.UtcNow;
            }
            await products.InsertManyAsync(productlist);
        }
    }
}
