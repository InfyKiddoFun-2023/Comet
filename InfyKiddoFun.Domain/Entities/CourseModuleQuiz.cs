namespace InfyKiddoFun.Domain.Entities;

public class CourseModuleQuiz
{
    public string Id { get; set; }
    
    public string Title { get; set; }
    
    public CourseModule Module { get; set; }
    public string ModuleId { get; set; }
    
    public IList<CourseModuleQuizQuestion> Questions { get; set; }
    
    public int PassPercentage { get; set; }
}