namespace InfyKiddoFun.Domain.Entities;

public class CourseProgress
{
    public CourseProgress()
    {
        CourseModules = new List<CourseModuleProgress>();
    }
    
    public string Id { get; set; }
    
    public Course Course { get; set; }
    public string CourseId { get; set; }
    
    public StudentUser Student { get; set; }
    public string StudentId { get; set; }
    
    public bool Completed { get; set; }
    public DateTime? CompletedOn { get; set; }
    
    public ICollection<CourseModuleProgress> CourseModules { get; set; }

    public DateTime LastUpdated { get; set; }
}