namespace InfyKiddoFun.Application.Models.Courses;

public class UpdateCourseModuleQuizRequest
{
    public string Id { get; set; }
    public string Title { get; set; }
    public int PassPercentage { get; set; }
    public string ModuleId { get; set; }
    public IList<QuizQuestionModel> Questions { get; set; }
}