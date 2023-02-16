namespace InfyKiddoFun.Domain.Entities;

public class ParentUser : AppUser
{
    public StudentUser Student { get; set; }
    public string StudentId { get; set; }
}