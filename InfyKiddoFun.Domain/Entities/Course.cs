using InfyKiddoFun.Domain.Enums;

namespace InfyKiddoFun.Domain.Entities;

public class Course
{
    public Course()
    {
        Enrollments = new List<CourseEnrollment>();
        Modules = new List<CourseModule>();
    }
    
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int DurationInWeeks { get; set; }

    public AgeGroup AgeGroup { get; set; }

    public DifficultyLevel DifficultyLevel { get; set; }

    public SpecificStream SpecificStream { get; set; }

    public ICollection<CourseEnrollment> Enrollments { get; set; }
    
    public ICollection<CourseModule> Modules { get; set; }
}