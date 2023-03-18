using InfyKiddoFun.Domain.Enums;

namespace InfyKiddoFun.Application.Models.Courses;

public class CreateCourseModuleMaterialRequest
{
    public string ModuleId { get; set; }
    public int Order { get; set; }
    public string Title { get; set; }
    public MaterialType MaterialType { get; set; }
    public string Link { get; set; }
}