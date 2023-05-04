using VeeArc.Application.Common.Mappings;
using VeeArc.Domain.Entities;
using DomainUser = VeeArc.Domain.Entities.User;

namespace VeeArc.Application.Feature.User;

public class UserResponse : IMapFrom<DomainUser>
{
    public int Id { get; set; }
    
    public string Username { get; set; }
    
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }
    
    public List<Role> Roles { get; set; }
}
