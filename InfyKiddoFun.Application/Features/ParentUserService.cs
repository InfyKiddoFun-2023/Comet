using InfyKiddoFun.Application.Interfaces;
using InfyKiddoFun.Application.Models.Identity;
using InfyKiddoFun.Domain.Wrapper;
using InfyKiddoFun.Infrastructure;

namespace InfyKiddoFun.Application.Features;

public class ParentUserService : IParentUserService
{
    private readonly AppDbContext _appDbContext;

    public ParentUserService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<IResult<LoginResponse>> LoginAsync(LoginRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<IResult<LoginResponse>> RefreshTokenAsync(RefreshTokenRequest request)
    {
        throw new NotImplementedException();
    }
}