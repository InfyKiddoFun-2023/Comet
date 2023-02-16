using InfyKiddoFun.Application.Interfaces;
using InfyKiddoFun.Application.Models.Identity;
using InfyKiddoFun.Domain.Entities;
using InfyKiddoFun.Domain.Wrapper;
using Microsoft.AspNetCore.Identity;

namespace InfyKiddoFun.Application.Features;

public class ParentUserService : IParentUserService
{
    private readonly UserManager<ParentUser> _userManager;

    public ParentUserService(UserManager<ParentUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IResult<LoginResponse>> LoginAsync(LoginRequest request)
    {
        try
        {
            throw new Exception("Not implemented");
        }
        catch (Exception e)
        {
            return await Result<LoginResponse>.FailAsync(e.Message);
        }
    }

    public async Task<IResult<LoginResponse>> RefreshTokenAsync(RefreshTokenRequest request)
    {
        try
        {
            throw new Exception("Not implemented");
        }
        catch (Exception e)
        {
            return await Result<LoginResponse>.FailAsync(e.Message);
        }
    }
}