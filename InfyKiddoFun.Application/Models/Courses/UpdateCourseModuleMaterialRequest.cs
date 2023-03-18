using InfyKiddoFun.Domain.Enums;

namespace InfyKiddoFun.Application.Models.Courses;

public class UpdateCourseModuleMaterialRequest
{
    public string Id { get; set; }
    public string CourseModuleId { get; set; }
    public int Order { get; set; }
    public string Title { get; set; }
    public MaterialType MaterialType { get; set; }
    public string Link { get; set; }
}