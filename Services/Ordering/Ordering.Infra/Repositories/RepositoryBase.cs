using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;
using Ordering.Infra.Data;

namespace Ordering.Infra.Repositories;

public class RepositoryBase<T>(OrderContext orderContext) : IAsyncRepository<T> where T : EntityBase
{
    public async Task<T> AddAsync(T entity)
    {
        orderContext.Set<T>().Add(entity);
        await orderContext.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(T entity)
    {
        orderContext.Set<T>().Remove(entity);
        await orderContext.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await orderContext.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
    {
        return await orderContext.Set<T>().Where(predicate).ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await orderContext.Set<T>().FindAsync(id);
    }

    public async Task UpdateAsync(T entity)
    {
        orderContext.Entry(entity).State = EntityState.Modified;
        await orderContext.SaveChangesAsync();
    }
}
