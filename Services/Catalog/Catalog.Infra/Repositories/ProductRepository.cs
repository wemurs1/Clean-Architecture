using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Core.Specifications;
using Catalog.Infra.Options;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Infra.Repositories;

public class ProductRepository(IOptions<DatabaseSettings> settings)
    : BaseRepository<Product>(settings.Value, settings.Value.ProductCollectionName), IProductRepository
{
    public async Task<ProductBrand> GetBrandsByIdAsync(string brandId)
    {
        return await _brandCollection.Find(x => x.Id == brandId).FirstOrDefaultAsync();
    }

    public async Task<Pagination<Product>> GetProductsAsync(CatalogSpecParams catalogSpecParams)
    {
        var builder = Builders<Product>.Filter;
        var filter = builder.Empty;
        if (!string.IsNullOrEmpty(catalogSpecParams.Search)) filter &= builder.Where(p => p.Name.ToLower().Contains(catalogSpecParams.Search.ToLower()));
        if (!string.IsNullOrEmpty(catalogSpecParams.BrandId)) filter &= builder.Eq(p => p.Brand.Id, catalogSpecParams.BrandId);
        if (!string.IsNullOrEmpty(catalogSpecParams.TypeId)) filter &= builder.Eq(p => p.Type.Id, catalogSpecParams.TypeId);

        var totalItems = await _productCollection.CountDocumentsAsync(filter);
        var data = await ApplyDataFilters(catalogSpecParams, filter);
        return new Pagination<Product>(catalogSpecParams.PageIndex, catalogSpecParams.PageSize, (int)totalItems, data);
    }

    public async Task<IEnumerable<Product>> GetProductsByBrand(string name)
    {
        return await _productCollection.Find(x => x.Brand.Name.ToLower() == name.ToLower()).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByName(string name)
    {
        var filter = Builders<Product>.Filter.Regex(p => p.Name, new BsonRegularExpression($".*{name}.*", "i"));
        return await _productCollection.Find(filter).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByType(string name)
    {
        return await _productCollection.Find(x => x.Type.Name.ToLower() == name.ToLower()).ToListAsync();
    }

    public async Task<ProductType> GetTypesByIdAsync(string typeId)
    {
        return await _typeCollection.Find(x => x.Id == typeId).FirstOrDefaultAsync();
    }

    private async Task<IReadOnlyCollection<Product>> ApplyDataFilters(CatalogSpecParams catalogSpecParams, FilterDefinition<Product> filter)
    {
        var sortDefinition = Builders<Product>.Sort.Ascending("Name");
        if (!string.IsNullOrEmpty(catalogSpecParams.Sort))
        {
            sortDefinition = catalogSpecParams.Sort switch
            {
                "priceAsc" => Builders<Product>.Sort.Ascending(p => p.Price),
                "priceDesc" => Builders<Product>.Sort.Descending(p => p.Price),
                _ => Builders<Product>.Sort.Ascending(p => p.Name)
            };
        }
        return await _productCollection
            .Find(filter)
            .Sort(sortDefinition)
            .Skip(catalogSpecParams.PageSize * (catalogSpecParams.PageIndex - 1))
            .Limit(catalogSpecParams.PageSize)
            .ToListAsync();
    }
}
