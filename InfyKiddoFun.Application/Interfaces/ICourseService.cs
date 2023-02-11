using InfyKiddoFun.Application.Models;

namespace InfyKiddoFun.Application.Interfaces;

public interface ICourseService
{
    void AddCourse(AddEditCourseModel model);
    void UpdateCourse(AddEditCourseModel model);
}