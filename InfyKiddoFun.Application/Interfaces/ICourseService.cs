using InfyKiddoFun.Application.Models.Courses;
using InfyKiddoFun.Domain.Wrapper;

namespace InfyKiddoFun.Application.Interfaces;

public interface ICourseService
{
    Task<PaginatedResult<CourseResponse>> GetCoursesAsync(int pageNumber, int pageSize, string searchQuery);
    Task<IResult<CourseFullResponse>> GetCourseAsync(string courseId);
}