﻿using InfyKiddoFun.Domain.Enums;

namespace InfyKiddoFun.Application.Models.Identity;

public class MentorRegisterRequest
{
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string AboutMe { get; set; }
    public Subject Subject { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}