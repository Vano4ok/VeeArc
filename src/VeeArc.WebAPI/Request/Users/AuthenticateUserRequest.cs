namespace VeeArc.WebAPI.Request.Users;

public record AuthenticateUserRequest
{
    public required string Username { get; set; }
    
    public required string Password { get; set; }
}