using Microsoft.AspNetCore.Mvc;
using VeeArc.Application.Feature.Authenticate;
using VeeArc.Application.Feature.User.Create;
using VeeArc.Application.Feature.User.Delete;
using VeeArc.Application.Feature.User.Update;
using VeeArc.Domain.Entities;

namespace VeeArc.WebAPI.Controllers;

[Route("users")]
public class UserController : ApiControllerBase
{
    [HttpPost] 
    public async Task<IActionResult> CreateUser(CreateUserCommand createUserCommand)
    {
        User user = await Mediator.Send(createUserCommand);
        
        return Ok(user);
    }
    
    [HttpPatch] 
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserCommand updateUserCommand)
    {
        if(id != updateUserCommand.Id)
        {
            return BadRequest();
        }    
        
        User user = await Mediator.Send(updateUserCommand);

        return Ok(user);
    }
    
    [HttpDelete] 
    public async Task<IActionResult> DeleteUser(int id)
    {
        var deleteCommand = new DeleteUserCommand()
        {
            Id = id
        };
        
        await Mediator.Send(deleteCommand);

        return NoContent();
    }
    
    [HttpGet] 
    public async Task<IActionResult> GetUser(int id){
        return Ok();
    }
    
    [HttpPost("authenticate")]
    public async Task<IActionResult> AuthenticateUser(AuthenticateCommand authenticateCommand)
    {
        Jwt token = await Mediator.Send(authenticateCommand);
        
        return Ok(token);
    }
}