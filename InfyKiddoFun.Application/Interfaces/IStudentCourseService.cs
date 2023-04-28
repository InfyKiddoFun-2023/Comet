using InfyKiddoFun.Application.Models.Courses;
using InfyKiddoFun.Domain.Wrapper;

namespace InfyKiddoFun.Application.Interfaces;

public interface IStudentCourseService
{
    Task<PaginatedResult<CourseResponse>> GetEnrolledCoursesAsync(int pageNumber, int pageSize, string searchString);
    Task<IResult<bool>> IsEnrolledInCourseAsync(string courseId);
    Task<IResult> EnrollCourseAsync(string courseId);
    Task<IResult> MarkCourseModuleCompletedAsync(string courseModuleId);
    Task<IResult<QuizAttemptResponse>> AttemptQuizAsync(QuizAttemptRequest request);
    Task<IResult<QuizAttemptResponse>> GetQuizAttemptResultAsync(string quizId);

}