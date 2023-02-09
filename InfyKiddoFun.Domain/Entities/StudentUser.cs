using InfyKiddoFun.Domain.Enums;

namespace InfyKiddoFun.Domain.Entities;

public class StudentUser : AppUser
{
    public AgeGroup AgeGroup { get; set; }
}