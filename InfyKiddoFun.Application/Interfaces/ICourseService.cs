using InfyKiddoFun.Application.Models.Courses;
using InfyKiddoFun.Domain.Wrapper;

namespace InfyKiddoFun.Application.Interfaces;

public interface ICourseService
{
    Task<PaginatedResult<CourseResponse>> GetCoursesBySubjectAsync(int pageNumber, int pageSize, int subject, string searchQuery);
    Task<PaginatedResult<CourseResponse>> GetCoursesByAgeGroupAsync(int pageNumber, int pageSize, int ageGroup, string searchQuery);
    Task<PaginatedResult<CourseResponse>> GetCoursesAsync(int pageNumber, int pageSize, string searchQuery);
    Task<IResult<CourseFullResponse>> GetCourseAsync(string courseId);
}