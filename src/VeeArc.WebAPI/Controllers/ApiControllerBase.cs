using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VeeArc.WebAPI.Controllers;

[ApiController]
public class ApiControllerBase : ControllerBase
{
    private readonly Lazy<ISender> _mediator;

    protected ApiControllerBase()
    {
        _mediator = new Lazy<ISender>(InitializeMediator);
    }

    protected ISender Mediator => _mediator.Value;

    private ISender InitializeMediator()
    {
        ISender mediator = HttpContext.RequestServices.GetRequiredService<ISender>();

        return mediator;
    }
}
