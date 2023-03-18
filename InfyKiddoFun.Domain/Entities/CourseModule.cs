namespace InfyKiddoFun.Domain.Entities;

public class CourseModule
{
    public string Id { get; set; }
    
    public int Order { get; set; }
    
    public string Title { get; set; }
    
    public Course Course { get; set; }
    public string CourseId { get; set; }
    
    public string Description { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public IList<CourseModuleMaterial> Materials { get; set; }
    
    public CourseModuleQuiz Quiz { get; set; }
}