namespace InfyKiddoFun.Application.Models.Courses;

public class CreateCourseModuleQuizRequest
{
    public string Title { get; set; }
    public int PassPercentage { get; set; }
    public string ModuleId { get; set; }
    public IList<QuizQuestionModel> Questions { get; set; }
}