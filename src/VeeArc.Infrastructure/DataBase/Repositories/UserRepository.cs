using Microsoft.EntityFrameworkCore;
using VeeArc.Application.Common.Interfaces;
using VeeArc.Domain.Entities;
using VeeArc.Infrastructure.Common.Interfaces;

namespace VeeArc.Infrastructure.DataBase.Repositories;

public class UserRepository : BaseDbRepository<User>, IUserRepository
{
    public UserRepository(IApplicationDbContext dbContext) : base(dbContext, dbContext.Users)
    {
    }

    public override Task<User?> GetByIdAsync(int id)
    {
        return DbSet.Include(user => user.Roles)
                    .Include(user => user.Articles)
                    .FirstOrDefaultAsync(user => user.Id == id);
    }

    public override IQueryable<User> GetAll()
    {
        return DbSet.Include(user => user.Roles);
    }

    public Task<User?> GetByUsernameAsync(string username)
    {
        return DbSet.Include(user => user.Roles)
                    .FirstOrDefaultAsync(user => user.Username == username);
    }

    public Task<User?> GetByEmailAsync(string email)
    {
        return DbSet.Include(user => user.Roles)
                    .FirstOrDefaultAsync(user => user.Email == email);
    }
}
