﻿using InfyKiddoFun.Application.Interfaces;
using InfyKiddoFun.Application.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InfyKiddoFun.API.Controllers;

[AllowAnonymous]
[Route("api/users/parent")]
[ApiController]
public class ParentUserController : ControllerBase
{
    private readonly IParentUserService _parentUserService;

    public ParentUserController(IParentUserService parentUserService)
    {
        _parentUserService = parentUserService;
    }
    
    [HttpPost("login")]
    public async Task<ActionResult> LoginAsync(LoginRequest model)
    {
        return Ok(await _parentUserService.LoginAsync(model));
    }
    
    [HttpPost("refresh")]
    public async Task<ActionResult> RefreshTokenAsync(RefreshTokenRequest model)
    {
        return Ok(await _parentUserService.RefreshTokenAsync(model));
    }
}