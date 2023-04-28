namespace InfyKiddoFun.Application.Models.Courses;

public class QuizAttemptRequest
{
    public string CourseModuleId { get; set; }
    public string QuizId { get; set; }
    public IList<QuizQuestionAttemptRequest> Questions { get; set; }
}

public class QuizQuestionAttemptRequest
{
    public string QuestionId { get; set; }
    public string Answer { get; set; }
}