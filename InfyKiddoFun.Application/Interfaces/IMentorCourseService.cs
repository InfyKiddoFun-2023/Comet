using InfyKiddoFun.Application.Models.Courses;
using InfyKiddoFun.Domain.Wrapper;

namespace InfyKiddoFun.Application.Interfaces;

public interface IMentorCourseService
{
    //view created courses
    Task<PaginatedResult<CourseResponse>> GetCoursesAsync(int pageNumber, int pageSize, string searchQuery, string userId);
    
    Task<IResult> AddCourseAsync(CreateCourseRequest request, string userId);
    Task<IResult> UpdateCourseAsync(UpdateCourseRequest request, string userId);
    Task<IResult> DeleteCourseAsync(string courseId, string userId);
}