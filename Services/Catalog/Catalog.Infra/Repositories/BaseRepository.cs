using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infra.Options;
using MongoDB.Driver;

namespace Catalog.Infra.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    private readonly IMongoDatabase _database;
    private readonly IMongoCollection<T> _collection;
    protected readonly IMongoCollection<Product> _productCollection;
    protected readonly IMongoCollection<ProductBrand> _brandCollection;
    protected readonly IMongoCollection<ProductType> _typeCollection;

    public BaseRepository(DatabaseSettings settings, string? collectionName)
    {
        _database = new MongoClient(settings.ConnectionString)
            .GetDatabase(settings.DatabaseName);

        _collection = _database.GetCollection<T>(collectionName);
        _brandCollection = _database.GetCollection<ProductBrand>(settings.BrandCollectionName);
        _typeCollection = _database.GetCollection<ProductType>(settings.TypeCollectionName);
        _productCollection = _database.GetCollection<Product>(settings.ProductCollectionName);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<T?> GetByIdASync(string id)
    {
        return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var deleted = await _collection.DeleteOneAsync(x => x.Id == id);
        return deleted.IsAcknowledged && deleted.DeletedCount > 0;
    }

    public async Task<bool> UpdateAsync(T item)
    {
        var updated = await _collection.ReplaceOneAsync(x => x.Id == item.Id, item);
        return updated.IsAcknowledged && updated.ModifiedCount > 0;
    }

    public async Task<T> CreateAsync(T item)
    {
        await _collection.InsertOneAsync(item);
        return item;
    }
}
