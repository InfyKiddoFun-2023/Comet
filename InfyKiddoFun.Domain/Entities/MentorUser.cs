using InfyKiddoFun.Domain.Enums;

namespace InfyKiddoFun.Domain.Entities;

public class MentorUser : AppUser
{
    public MentorUser()
    {
        Courses = new List<Course>();
    }
    
    public Subject Subject { get; set; }
    
    public ICollection<Course> Courses { get; set; }
}