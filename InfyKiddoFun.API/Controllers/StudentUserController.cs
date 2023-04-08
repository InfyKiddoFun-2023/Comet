using InfyKiddoFun.Application.Interfaces;
using InfyKiddoFun.Application.Models.Identity;
using InfyKiddoFun.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InfyKiddoFun.API.Controllers;

[Authorize(Roles = Roles.Student)]
[Route("api/users/student")]
[ApiController]
public class StudentUserController : ControllerBase
{
    private readonly IStudentUserService _studentUserService;

    public StudentUserController(IStudentUserService studentUserService)
    {
        _studentUserService = studentUserService;
    }

    [AllowAnonymous]
    [HttpPost("token/get")]
    public async Task<IActionResult> LoginAsync(LoginRequest request)
    {
        return Ok(await _studentUserService.LoginAsync(request));
    }
    
    [AllowAnonymous]
    [HttpPost("token/refresh")]
    public async Task<IActionResult> RefreshTokenAsync(RefreshTokenRequest request)
    {
        return Ok(await _studentUserService.RefreshTokenAsync(request));
    }
    
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(StudentRegisterRequest request)
    {
        return Ok(await _studentUserService.RegisterAsync(request));
    }
    
    [HttpPost("info/update")]
    public async Task<IActionResult> UpdateInfoAsync(UpdateStudentInfoRequest request)
    {
        return Ok(await _studentUserService.UpdateInfoAsync(request));
    }
    
    [HttpPost("password/update")]
    public async Task<IActionResult> UpdatePasswordAsync(UpdatePasswordRequest request)
    {
        return Ok(await _studentUserService.UpdatePasswordAsync(request));
    }
}