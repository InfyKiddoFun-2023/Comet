namespace InfyKiddoFun.Application.Models.Courses;

public class UpdateCourseModuleRequest
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Order { get; set; }
    public DateTime StartDate { get; set; }
    public string CourseId { get; set; }
}