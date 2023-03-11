using InfyKiddoFun.Domain.Wrapper;

namespace InfyKiddoFun.Application.Interfaces;

public interface IStudentCourseService
{
    Task<IResult<bool>> IsEnrolledInCourseAsync(string courseId, string userId);
    Task<IResult> EnrollCourseAsync(string courseId, string userId);
}