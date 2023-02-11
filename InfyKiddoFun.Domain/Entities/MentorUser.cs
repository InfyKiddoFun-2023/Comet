using InfyKiddoFun.Domain.Enums;

namespace InfyKiddoFun.Domain.Entities;

public class MentorUser : AppUser
{
    public SpecificStream SpecificStream { get; set; }
}