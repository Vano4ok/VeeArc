using System.Security.Claims;
using VeeArc.Application.Common.Interfaces;

namespace VeeArc.WebAPI.Services;

public class CurrentUserService : ICurrentUserService
{
    public int? UserId { get; private set; }

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        SetUserId(httpContextAccessor);
    }

    private void SetUserId(IHttpContextAccessor httpContextAccessor)
    {
        string? contextUserId = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (contextUserId is null)
        {
            return;
        }
        
        if (int.TryParse(contextUserId, out int userId))
        {
            UserId = userId;
        }
    }
}
