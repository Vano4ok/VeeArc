namespace VeeArc.WebAPI.Request.Users;

public record CreateUserRequest
{
    public required int Id { get; init; }
    
    public required string FirstName { get; init; }
    
    public required string LastName { get; init; }
    
    public required string Username { get; init; }
    
    public required string Email { get; init; }
    
    public required string Password { get; init; }
}