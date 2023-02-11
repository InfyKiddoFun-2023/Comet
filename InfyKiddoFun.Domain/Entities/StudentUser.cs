using InfyKiddoFun.Domain.Enums;

namespace InfyKiddoFun.Domain.Entities;

public class StudentUser : AppUser
{
    public string Id { get; set; }
    public string Name { get; set; }
    public AgeGroup AgeGroup { get; set; }

    public SpecificStream SpecificStream { get; set; }
}