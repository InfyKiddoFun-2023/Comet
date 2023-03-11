using InfyKiddoFun.Application.Models.Identity;
using InfyKiddoFun.Domain.Wrapper;

namespace InfyKiddoFun.Application.Interfaces;

public interface IMentorUserService
{
    Task<IResult<LoginResponse>> LoginAsync(LoginRequest request);
    Task<IResult<LoginResponse>> RefreshTokenAsync(RefreshTokenRequest request);
    Task<IResult> RegisterAsync(MentorRegisterRequest request);
}