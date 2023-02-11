using InfyKiddoFun.Domain.Enums;

namespace InfyKiddoFun.Domain.Entities;

public class MentorUser : AppUser
{
    public string Id { get; set; }
    public string Name { get; set; }
    public SpecificStream SpecificStream { get; set; }
}