namespace InfyKiddoFun.Domain.Entities;

public class CourseModuleQuizAttempt
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public StudentUser User { get; set; }
    public string CourseModuleId { get; set; }
    public CourseModule CourseModule { get; set; }
    public string QuizId { get; set; }
    public CourseModuleQuiz Quiz { get; set; }
    public int TotalQuestions { get; set; }
    public int TotalCorrectAnswers { get; set; }
    public int TotalWrongAnswers { get; set; }
    public int TotalUnanswered { get; set; }
    public int TotalScore { get; set; }
    public int TotalMarks { get; set; }
    public int Percentage { get; set; }
    public bool IsPassed { get; set; }
    public string QuizTitle { get; set; }
    public int PassPercentage { get; set; }
    
    public IList<CourseModuleQuizQuestionAttempt> Questions { get; set; }
}