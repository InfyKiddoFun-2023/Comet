using InfyKiddoFun.Application.Interfaces;
using InfyKiddoFun.Application.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InfyKiddoFun.API.Controllers;

[Authorize]
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
}