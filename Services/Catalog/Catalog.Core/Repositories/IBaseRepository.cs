using Catalog.Core.Entities;

namespace Catalog.Core.Repositories;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdASync(string id);
    Task<bool> DeleteAsync(string id);
    Task<bool> UpdateAsync(T item);
    Task<T> CreateAsync(T item);
}
