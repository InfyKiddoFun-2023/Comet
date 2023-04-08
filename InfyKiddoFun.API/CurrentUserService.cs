using System.Security.Claims;
using InfyKiddoFun.Application.Interfaces;
using InfyKiddoFun.Domain.Constants;

namespace InfyKiddoFun.API;

public class CurrentUserService : ICurrentUserService
{
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ApplicationClaimTypes.Id);
        UserName = httpContextAccessor.HttpContext?.User?.FindFirstValue(ApplicationClaimTypes.UserName);
        Role = httpContextAccessor.HttpContext?.User?.FindFirstValue(ApplicationClaimTypes.Role);
        IsAuthenticated = httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
    }

    public string UserId { get; }
    public string UserName { get; }
    public string Role { get; }
    public bool IsAuthenticated { get; }
}