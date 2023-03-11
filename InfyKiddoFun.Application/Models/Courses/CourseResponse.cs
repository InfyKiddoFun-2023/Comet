using InfyKiddoFun.Domain.Entities;
using InfyKiddoFun.Domain.Enums;

namespace InfyKiddoFun.Application.Models.Courses;

public class CourseResponse
{
    public string Id { get; set; }
    
    public string Title { get; set; }
    
    public string MentorName { get; set; }
    public string MentorId { get; set; }
    
    public string Description { get; set; }
    
    public string AgeGroup { get; set; }
    
    public string DifficultyLevel { get; set; }
    
    public string Stream { get; set; }
    
    public int Enrollments { get; set; }
}