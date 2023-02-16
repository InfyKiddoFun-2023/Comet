﻿namespace InfyKiddoFun.Application.Models.Identity;

public class LoginResponse
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryDate { get; set; }
}