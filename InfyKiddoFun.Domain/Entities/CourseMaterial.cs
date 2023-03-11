using InfyKiddoFun.Domain.Enums;

namespace InfyKiddoFun.Domain.Entities;

public class CourseMaterial
{
    public string Id { get; set; }
    
    public Course Course { get; set; }
    public string CourseId { get; set; }
    
    public MaterialType MaterialType { get; set; }
    
    public string Link { get; set; }
}