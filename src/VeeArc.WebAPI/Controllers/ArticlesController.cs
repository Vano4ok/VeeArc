using Microsoft.AspNetCore.Mvc;
using VeeArc.Application.Feature.Articles;
using VeeArc.Application.Feature.Articles.Create;
using VeeArc.Application.Feature.Articles.Delete;
using VeeArc.Application.Feature.Articles.Get;
using VeeArc.Application.Feature.Articles.GetAll;
using VeeArc.WebAPI.Request.Articles;

namespace VeeArc.WebAPI.Controllers;

[Route("articles")]
public class ArticlesController : ApiControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GettAll()
    {
        var query = new GetArticlesQuery();

        List<ArticleResponse> response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var query = new GetArticleQuery
        {
            Id = id,
        };

        ArticleResponse articleResponse = await Mediator.Send(query);

        return Ok(articleResponse);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateArticleRequest request)
    {
        var command = new CreateArticleCommand
        {
            Title = request.Title,
            Text = request.Text,
        };

        ArticleResponse articleResponse = await Mediator.Send(command);

        return Ok(articleResponse);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var command = new DeleteArticleCommand
        {
            Id = id,
        };

        await Mediator.Send(command);

        return NoContent();
    }
}
