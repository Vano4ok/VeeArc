namespace VeeArc.Application.Feature.User;

public record UserResponse
{
    public required int Id { get; init; }
    
    public required string FirstName { get; init; }

    public required string LastName { get; init; }

    public required string Email { get; init; } 
    
    public required string Password { get; init; }
}