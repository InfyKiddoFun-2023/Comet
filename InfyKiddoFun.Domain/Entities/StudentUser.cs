using InfyKiddoFun.Domain.Enums;

namespace InfyKiddoFun.Domain.Entities;

public class StudentUser : AppUser
{
    public AgeGroup AgeGroup { get; set; }
    public SpecificStream SpecificStream { get; set; }

    public ICollection<Enrollment> Enrollments { get; set; }
}