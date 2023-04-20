using VeeArc.Domain.Entities;

namespace VeeArc.Application.Common.Interfaces;

public interface IArticleRepository : IBaseRepository<Article>
{
    Task<List<Article>> GetAll(CancellationToken cancellationToken);
}
