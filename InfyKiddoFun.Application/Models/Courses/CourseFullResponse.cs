using InfyKiddoFun.Domain.Enums;

namespace InfyKiddoFun.Application.Models.Courses;

public class CourseFullResponse
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string MentorName { get; set; }
    public string MentorId { get; set; }
    public string AgeGroup { get; set; }
    public string Stream { get; set; }
    public string DifficultyLevel { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime StartDate { get; set; }
    public int Enrollments { get; set; }
    public IList<CourseMaterialResponse> Materials { get; set; }
    public IList<CourseModuleResponse> Modules { get; set; }
}