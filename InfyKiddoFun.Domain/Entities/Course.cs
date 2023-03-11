using InfyKiddoFun.Domain.Enums;

namespace InfyKiddoFun.Domain.Entities;

public class Course
{
    //course basic info
    public string Id { get; set; }
    
    public string Title { get; set; }
    
    public MentorUser Mentor { get; set; }
    public string MentorId { get; set; }
    
    public string Description { get; set; }
    
    public AgeGroup AgeGroup { get; set; }
    
    public DifficultyLevel DifficultyLevel { get; set; }
    
    public SpecificStream Stream { get; set; }
    
    public DateTime CreatedDate { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public IList<CourseEnrollment> Enrollments { get; set; }
    
    public IList<CourseMaterial> Materials { get; set; }
}