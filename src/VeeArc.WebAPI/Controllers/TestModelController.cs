using Microsoft.AspNetCore.Mvc;
using VeeArc.Application.Feature.TestModels.Create;
using VeeArc.Application.Feature.TestModels.Update;
using VeeArc.WebAPI.Request.TestModels;

namespace VeeArc.WebAPI.Controllers;

[Route("testmodels")]
public class TestModelController : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateTestModelRequest request)
    {
        var command = new CreateTestModelCommand()
        {
            Title = request.Title,
            Text = request.Text,
        };

        var result = await Mediator.Send(command);

        return Ok(result);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateTestModelRequest request)
    {
        var command = new UpdateTestModelCommand()
        {
            Id = id,
            Title = request.Title,
            Text = request.Text,
            PageIndex = request.PageIndex,
        };

        var result = await Mediator.Send(command);

        return Ok(result);
    }
}
