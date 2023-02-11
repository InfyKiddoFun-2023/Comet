using InfyKiddoFun.Application.Models;

namespace InfyKiddoFun.Application.Interfaces;

public interface ICourseService
{
    Task<List<CourseResponseModel>> GetAllCourse(int pageNumber, int pageSize, string searchString);
    CourseResponseModel GetById(string id);
    void AddCourse(AddEditCourseModel model);
    void UpdateCourse(AddEditCourseModel model);
    void DeleteCourse(string id);
}