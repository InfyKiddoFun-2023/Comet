using InfyKiddoFun.Application.Interfaces;
using InfyKiddoFun.Application.Models;
using InfyKiddoFun.Domain.Entities;
using InfyKiddoFun.Infrastructure;

namespace InfyKiddoFun.Application.Features;

public class CourseService : ICourseService
{
    private readonly AppDbContext _appDbContext;

    public CourseService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    
    public void AddCourse(AddEditCourseModel model)
    {
        var course = new Course()
        {
            AgeGroup = model.AgeGroup,
            Duration = model.Duration,
            Description = model.Description,
            DifficultyLevel = model.DifficultyLevel,
            Name = model.Name,
            SpecificStream = model.SpecificStream
        };
        _appDbContext.Courses.Add(course);
        _appDbContext.SaveChanges();
    }
    
    public void UpdateCourse(AddEditCourseModel model)
    {
        var existingCourse = _appDbContext.Courses.Find(model.Id);
        var course = new Course()
        {
            AgeGroup = model.AgeGroup,
            Duration = model.Duration,
            Description = model.Description,
            DifficultyLevel = model.DifficultyLevel,
            Name = model.Name,
            SpecificStream = model.SpecificStream
        };
        _appDbContext.Courses.Update(course);
        _appDbContext.SaveChanges();
    }
}