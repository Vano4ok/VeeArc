using Microsoft.AspNetCore.Mvc;
using VeeArc.Application.Feature.Authenticate;
using VeeArc.Application.Feature.User;
using VeeArc.Application.Feature.User.Create;
using VeeArc.Application.Feature.User.Delete;
using VeeArc.Application.Feature.User.Get;
using VeeArc.Application.Feature.User.Update;
using VeeArc.WebAPI.Request.Users;

namespace VeeArc.WebAPI.Controllers;

[Route("users")]
public class UserController : ApiControllerBase
{
    [HttpPost] 
    public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
    {
        var command = new CreateUserCommand()
        {
            Username = request.Username,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Password = request.Password
        };
        
        UserResponse user = await Mediator.Send(command);
        
        return Ok(user);
    }
    
    [HttpPatch("{id}")] 
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateUserRequest request)
    {
        var command = new UpdateUserCommand()
        {
            Id = id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Password = request.Password
        };
        
        UserResponse user = await Mediator.Send(command);

        return Ok(user);
    }
    
    [HttpDelete("{id}")] 
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var deleteCommand = new DeleteUserCommand()
        {
            Id = id
        };
        
        await Mediator.Send(deleteCommand);

        return NoContent();
    }
    
    [HttpGet("{id}")] 
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var query = new GetUserQuery()
        {
            Id = id
        };
        
        UserResponse user = await Mediator.Send(query);
        
        return Ok(user);
    }
    
    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate([FromBody] AuthenticateUserRequest request)
    {
        var command = new AuthenticateCommand()
        {
            Password = request.Password,
            Username = request.Username
        };
        
        Jwt token = await Mediator.Send(command);
        
        return Ok(token);
    }
}
