using InfyKiddoFun.Domain.Wrapper;

namespace InfyKiddoFun.Application.Interfaces;

public interface IStudentCourseService
{
    Task<IResult<bool>> IsEnrolledInCourseAsync(string courseId);
    Task<IResult> EnrollCourseAsync(string courseId);
    Task<IResult> MarkCourseModuleCompletedAsync(string courseModuleId);
    
}