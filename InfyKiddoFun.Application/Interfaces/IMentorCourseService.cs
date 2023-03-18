using InfyKiddoFun.Application.Models.Courses;
using InfyKiddoFun.Domain.Wrapper;

namespace InfyKiddoFun.Application.Interfaces;

public interface IMentorCourseService
{
    Task<PaginatedResult<CourseResponse>> GetCoursesAsync(int pageNumber, int pageSize, string searchQuery, string userId);
    Task<IResult> AddCourseAsync(CreateCourseRequest request, string userId);
    Task<IResult> UpdateCourseAsync(UpdateCourseRequest request, string userId);
    Task<IResult> DeleteCourseAsync(string courseId, string userId);
    Task<IResult> AddCourseModuleAsync(CreateCourseModuleRequest request, string userId);
    Task<IResult> UpdateCourseModuleAsync(UpdateCourseModuleRequest request, string userId);
    Task<IResult> DeleteCourseModuleAsync(string moduleId, string userId);
    Task<IResult> AddCourseModuleMaterialAsync(CreateCourseModuleMaterialRequest request, string userId);
    Task<IResult> UpdateCourseModuleMaterialAsync(UpdateCourseModuleMaterialRequest request, string userId);
    Task<IResult> DeleteCourseModuleMaterialAsync(string materialId, string userId);
    Task<IResult> AddCourseModuleQuizAsync(CreateCourseModuleQuizRequest request, string userId);
    Task<IResult> UpdateCourseModuleQuizAsync(UpdateCourseModuleQuizRequest request, string userId);
    Task<IResult> DeleteCourseModuleQuizAsync(string quizId, string userId);
}