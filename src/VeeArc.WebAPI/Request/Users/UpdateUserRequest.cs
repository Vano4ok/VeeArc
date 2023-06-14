
namespace VeeArc.WebAPI.Request.Users;

public record UpdateUserRequest
{
    public string FirstName { get; init; }

    public string LastName { get; init; }

    public string Email { get; init; } 
    
    public string Password { get; init; }
}
