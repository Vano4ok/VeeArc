using Microsoft.EntityFrameworkCore;
using VeeArc.Application.Common.Interfaces;
using VeeArc.Domain.Entities;
using VeeArc.Infrastructure.Common.Interfaces;

namespace VeeArc.Infrastructure.DataBase.Repositories;

public class ArticleRepository : BaseDbRepository<Article>, IArticleRepository
{
    public ArticleRepository(IApplicationDbContext dbContext) : base(dbContext, dbContext.Articles)
    {
    }
}
