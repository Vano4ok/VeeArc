using VeeArc.Domain.Entities;

namespace VeeArc.Application.Common.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetByUsernameAsync(string username);
    
    Task<User?> GetByEmailAsync(string email);
    
    Task<List<User>> GetAll();
}
