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
        if (int.TryParse(httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier),
                out int userId))
        {
            UserId = userId;
        }
    }
}