using Microsoft.EntityFrameworkCore;
using VeeArc.Application.Common.Interfaces;
using VeeArc.Domain.Common;
using VeeArc.Infrastructure.Common.Interfaces;

namespace VeeArc.Infrastructure.DataBase.Repositories;

public abstract class BaseDbRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    private readonly IApplicationDbContext _dbContext;

    protected readonly DbSet<T> DbSet;

    protected BaseDbRepository(IApplicationDbContext dbContext, DbSet<T> dbSet)
    {
        _dbContext = dbContext;
        DbSet = dbSet;
    }

    public void Update(T entity)
    {
        DbSet.Update(entity);
    }

    public async Task AddAsync(T entity)
    {
        await DbSet.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await DbSet.AddRangeAsync(entities);
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await DbSet.FindAsync(new object[] { id });
    }

    public void Remove(T entity)
    {
        DbSet.Remove(entity);
    }

    public async Task SaveAsync()
    {
        await _dbContext.SaveAsync();
    }
}
