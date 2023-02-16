using InfyKiddoFun.Application.Interfaces;
using InfyKiddoFun.Application.Models;
using InfyKiddoFun.Domain.Entities;
using InfyKiddoFun.Domain.Wrapper;
using InfyKiddoFun.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace InfyKiddoFun.Application.Features;

public class CourseService : ICourseService
{
    private readonly AppDbContext _appDbContext;

    public CourseService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<IResult<List<CourseResponseModel>>> GetAllCourse(int pageNumber, int pageSize, string searchString)
    {
        var courses = await _appDbContext.Courses
            .Select(x => new CourseResponseModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Duration = x.Duration,
                AgeGroup = x.AgeGroup,
                DifficultyLevel = x.DifficultyLevel,
                SpecificStream = x.SpecificStream
            })
            .OrderBy(x => x.Name)
            .Skip(pageNumber - 1)
            .Take(pageSize)
            .ToListAsync();
        return await Result<List<CourseResponseModel>>.SuccessAsync(courses);
    }

    public async Task<IResult<CourseResponseModel>> GetById(string id)
    {
        try
        {
            var course = await _appDbContext.Courses.FindAsync(id);
            if (course == null) 
                throw new Exception("Course not found!");
            var model = new CourseResponseModel
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                Duration = course.Duration,
                AgeGroup = course.AgeGroup,
                DifficultyLevel = course.DifficultyLevel,
                SpecificStream = course.SpecificStream
            };
            return await Result<CourseResponseModel>.SuccessAsync(model);
        }
        catch (Exception e)
        {
            return await Result<CourseResponseModel>.FailAsync(e.Message);
        }
    }

    public async Task<IResult> AddCourse(AddEditCourseModel model)
    {
        try
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
            await _appDbContext.SaveChangesAsync();
            return await Result.SuccessAsync("Added course successfully!");
        }
        catch (Exception e)
        {
            return await Result.FailAsync(e.Message);
        }
    }
    
    public async Task<IResult> UpdateCourse(AddEditCourseModel model)
    {
        try
        {
            var existingCourse = await _appDbContext.Courses.FindAsync(model.Id);
            if (existingCourse == null)
                throw new Exception("Course Not Found!");
            existingCourse.AgeGroup = model.AgeGroup;
            existingCourse.Duration = model.Duration;
            existingCourse.Description = model.Description;
            existingCourse.DifficultyLevel = model.DifficultyLevel;
            existingCourse.Name = model.Name;
            existingCourse.SpecificStream = model.SpecificStream;
            await _appDbContext.SaveChangesAsync();
            return await Result.SuccessAsync("Updated course successfully!");
        }
        catch (Exception e)
        {
            return await Result.FailAsync(e.Message);
        }
    }

    public async Task<IResult> DeleteCourse(string id)
    {
        try
        {
            var existingCourse = await _appDbContext.Courses.FindAsync(id);
            if (existingCourse == null)
                throw new Exception("Course Not Found!");
            _appDbContext.Courses.Remove(existingCourse);
            await _appDbContext.SaveChangesAsync();
            return await Result.SuccessAsync("Delete course successfully!");
        }
        catch (Exception e)
        {
            return await Result.FailAsync(e.Message);
        }
    }
}