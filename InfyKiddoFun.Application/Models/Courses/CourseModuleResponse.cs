namespace InfyKiddoFun.Application.Models.Courses;

public class CourseModuleResponse
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string CourseId { get; set; }
    public int Order { get; set; }
    public DateTime StartDate { get; set; }
    
    public IList<CourseModuleMaterialResponse> Materials { get; set; }
    public string QuizTitle { get; set; }
    public int QuizQuestions { get; set; }
}