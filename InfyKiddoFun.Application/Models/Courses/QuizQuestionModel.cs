namespace InfyKiddoFun.Application.Models.Courses;

public class QuizQuestionModel
{
    public string Question { get; set; }
    public List<string> Options { get; set; }
    public string CorrectOptions { get; set; }
}