namespace InfyKiddoFun.Domain.Entities;

public class CourseModuleQuizQuestion
{
    public string Id { get; set; }
    
    public string Question { get; set; }
    
    public CourseModuleQuiz Quiz { get; set; }
    public string QuizId { get; set; }
    
    public IList<string> Options { get; set; }
    
    public string CorrectOption { get; set; }
}