using InfyKiddoFun.Domain.Enums;

namespace InfyKiddoFun.Domain.Entities;

public class StudentUser : AppUser
{
    public StudentUser()
    {
        Enrollments = new List<CourseEnrollment>();
    }

    public AgeGroup AgeGroup { get; set; }
    
    public IList<Subject> PreferredSubjects { get; set; }

    public ICollection<CourseEnrollment> Enrollments { get; set; }
}