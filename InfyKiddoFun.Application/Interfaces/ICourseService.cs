using InfyKiddoFun.Application.Models;
using InfyKiddoFun.Domain.Wrapper;

namespace InfyKiddoFun.Application.Interfaces;

public interface ICourseService
{
    Task<IResult<List<CourseResponseModel>>> GetAllCourse(int pageNumber, int pageSize, string searchString);
    Task<IResult<CourseResponseModel>> GetById(string id);
    Task<IResult> AddCourse(AddEditCourseModel model);
    Task<IResult> UpdateCourse(AddEditCourseModel model);
    Task<IResult> DeleteCourse(string id);
}