using Catalog.Core.Entities;
using Catalog.Core.Specifications;

namespace Catalog.Core.Repositories;

public interface IProductRepository : IBaseRepository<Product>
{
    Task<Pagination<Product>> GetProductsAsync(CatalogSpecParams specParams);
    Task<IEnumerable<Product>> GetProductsByName(string name);
    Task<IEnumerable<Product>> GetProductsByBrand(string name);
    Task<IEnumerable<Product>> GetProductsByType(string name);
    Task<ProductBrand> GetBrandsByIdAsync(string brandId);
    Task<ProductType> GetTypesByIdAsync(string typeId);
}
