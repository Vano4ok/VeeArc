using VeeArc.Domain.Common;

namespace VeeArc.Application.Common.Interfaces;

public interface IBaseRepository<T> where T : BaseEntity
{
    void Update(T entity);

    Task AddAsync(T entity);

    Task AddRangeAsync(IEnumerable<T> entities);

    Task<T?> GetByIdAsync(int id);

    void Remove(T entity);

    Task SaveAsync();
}
