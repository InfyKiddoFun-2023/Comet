using InfyKiddoFun.Domain.Enums;

namespace InfyKiddoFun.Application.Models.Courses;

public class UpdateCourseRequest
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public AgeGroup AgeGroup { get; set; }
    public DifficultyLevel DifficultyLevel { get; set; }
    public Subject Subject { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime StartDate { get; set; }
}