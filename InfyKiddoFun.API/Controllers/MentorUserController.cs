﻿using InfyKiddoFun.Application.Interfaces;
using InfyKiddoFun.Application.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InfyKiddoFun.API.Controllers;

[Authorize]
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
}