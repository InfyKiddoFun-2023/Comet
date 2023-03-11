using System.Security.Claims;
using InfyKiddoFun.Application.Interfaces;
using InfyKiddoFun.Application.Models.Identity;
using InfyKiddoFun.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InfyKiddoFun.API.Controllers;

[Authorize(Roles = Roles.Mentor)]
[Route("api/users/mentor")]
[ApiController]
public class MentorUserController : ControllerBase
{
    private readonly IMentorUserService _mentorUserService;

    public MentorUserController(IMentorUserService mentorUserService)
    {
        _mentorUserService = mentorUserService;
    }
    
    [AllowAnonymous]
    [HttpPost("token/get")]
    public async Task<ActionResult> LoginAsync(LoginRequest model)
    {
        return Ok(await _mentorUserService.LoginAsync(model));
    }
    
    [AllowAnonymous]
    [HttpPost("token/refresh")]
    public async Task<ActionResult> RefreshTokenAsync(RefreshTokenRequest model)
    {
        return Ok(await _mentorUserService.RefreshTokenAsync(model));
    }
    
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult> RegisterAsync(MentorRegisterRequest model)
    {
        return Ok(await _mentorUserService.RegisterAsync(model));
    }
    
    [HttpPost("password/update")]
    public async Task<ActionResult> UpdatePasswordAsync(UpdatePasswordRequest model)
    {
        return Ok(await _mentorUserService.UpdatePasswordAsync(model, User.FindFirstValue(ApplicationClaimTypes.Id)));
    }
}