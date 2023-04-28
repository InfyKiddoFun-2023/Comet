namespace InfyKiddoFun.Application.Models.Courses;

public class QuizAttemptResponse
{
    public string QuizId { get; set; }
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
    public IList<QuizQuestionAttemptResponse> Questions { get; set; }
}

public class QuizQuestionAttemptResponse
{
    public string Question { get; set; }
    public string Answer { get; set; }
    public string CorrectAnswer { get; set; }
    public bool IsCorrect { get; set; }
}