using InfyKiddoFun.Application.Models.Identity;
using InfyKiddoFun.Domain.Wrapper;

namespace InfyKiddoFun.Application.Interfaces;

public interface IParentUserService
{
    Task<IResult<LoginResponse>> LoginAsync(LoginRequest request);
    Task<IResult<LoginResponse>> RefreshTokenAsync(RefreshTokenRequest request);
}