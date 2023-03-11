namespace InfyKiddoFun.Domain.Entities;

public class CourseProgress
{
    public string Id { get; set; }
    
    public Course Course { get; set; }
    public string CourseId { get; set; }
    
    public StudentUser Student { get; set; }
    public string StudentId { get; set; }
    
    public bool Completed { get; set; }
    public DateTime? CompletedOn { get; set; }
    
    public IList<CourseModuleProgress> CourseModules { get; set; }

    public DateTime LastUpdated { get; set; }
}