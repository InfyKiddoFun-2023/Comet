using InfyKiddoFun.Domain.Enums;

namespace InfyKiddoFun.Domain.Entities;

public class MentorUser : AppUser
{
    public MentorUser()
    {
        Courses = new List<Course>();
    }
    
    public SpecificStream SpecificStream { get; set; }
    
    public ICollection<Course> Courses { get; set; }
}