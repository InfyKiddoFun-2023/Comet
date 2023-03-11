using InfyKiddoFun.Domain.Enums;

namespace InfyKiddoFun.Domain.Entities;

public class CourseModuleMaterial
{
    public string Id { get; set; }
    
    public CourseModule Module { get; set; }
    public string ModuleId { get; set; }
    
    public MaterialType MaterialType { get; set; }
    
    public string Link { get; set; }
}