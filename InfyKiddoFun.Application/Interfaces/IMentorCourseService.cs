using InfyKiddoFun.Application.Models.Courses;
using InfyKiddoFun.Domain.Wrapper;

namespace InfyKiddoFun.Application.Interfaces;

public interface IMentorCourseService
{
    Task<PaginatedResult<CourseResponse>> GetCoursesAsync(int pageNumber, int pageSize, string searchQuery);
    Task<IResult> AddCourseAsync(CreateCourseRequest request);
    Task<IResult> UpdateCourseAsync(UpdateCourseRequest request);
    Task<IResult> DeleteCourseAsync(string courseId);
    Task<IResult> AddCourseModuleAsync(CreateCourseModuleRequest request);
    Task<IResult> UpdateCourseModuleAsync(UpdateCourseModuleRequest request);
    Task<IResult> DeleteCourseModuleAsync(string moduleId);
    Task<IResult> AddCourseModuleMaterialAsync(CreateCourseModuleMaterialRequest request);
    Task<IResult> UpdateCourseModuleMaterialAsync(UpdateCourseModuleMaterialRequest request);
    Task<IResult> DeleteCourseModuleMaterialAsync(string materialId);
    Task<IResult> AddCourseModuleQuizAsync(CreateCourseModuleQuizRequest request);
    Task<IResult> UpdateCourseModuleQuizAsync(UpdateCourseModuleQuizRequest request);
    Task<IResult> DeleteCourseModuleQuizAsync(string quizId);
}