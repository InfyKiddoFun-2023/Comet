namespace InfyKiddoFun.Application.Interfaces;

public interface ICurrentUserService
{
    string UserId { get; }
    string UserName { get; }
    string Role { get; }
    bool IsAuthenticated { get; }
}