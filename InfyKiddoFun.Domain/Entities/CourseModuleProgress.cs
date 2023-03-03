namespace InfyKiddoFun.Domain.Entities;

public class CourseModuleProgress
{
    public string Id { get; set; }
    
    public CourseProgress CourseProgress { get; set; }
    public string CourseProgressId { get; set; }
    
    public CourseModule CourseModule { get; set; }
    public string CourseModuleId { get; set; }
    
    public bool Completed { get; set; }
    public DateTime? CompletedOn { get; set; }

    public DateTime LastUpdated { get; set; }
}