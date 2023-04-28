namespace InfyKiddoFun.Domain.Entities;

public class CourseModuleQuizQuestionAttempt
{
    public string Id { get; set; }
    public string QuestionId { get; set; }
    public CourseModuleQuizQuestion Question { get; set; }
    public string Answer { get; set; }
    public string CorrectAnswer { get; set; }
    public bool IsCorrect { get; set; }
}