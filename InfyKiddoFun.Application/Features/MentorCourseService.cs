using InfyKiddoFun.Application.Extensions;
using InfyKiddoFun.Application.Interfaces;
using InfyKiddoFun.Application.Models.Courses;
using InfyKiddoFun.Domain.Entities;
using InfyKiddoFun.Domain.Wrapper;
using InfyKiddoFun.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace InfyKiddoFun.Application.Features;

public class MentorCourseService : IMentorCourseService
{
    private readonly AppDbContext _appDbContext;

    public MentorCourseService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    
    public async Task<PaginatedResult<CourseResponse>> GetCoursesAsync(int pageNumber, int pageSize, string searchQuery, string userId)
    {
        try
        {
            return await _appDbContext.Courses
                .Include(x => x.Mentor)
                .Include(x => x.Enrollments)
                .Where(x => x.MentorId == userId)
                .OrderByDescending(x => x.CreatedDate)
                .ThenBy(x => x.Title)
                .Select(x => new CourseResponse
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    MentorName = x.Mentor.FullName,
                    MentorId = x.MentorId,
                    AgeGroup = x.AgeGroup.ToDescriptionString(),
                    Stream = x.Subject.ToDescriptionString(),
                    DifficultyLevel = x.DifficultyLevel.ToDescriptionString(),
                    Enrollments = x.Enrollments.Count()
                })
                .ToPaginatedListAsync(pageNumber, pageSize);
        }
        catch (Exception e)
        {
            return PaginatedResult<CourseResponse>.Failure(new List<string>() { e.Message });
        }
    }

    public async Task<IResult> AddCourseAsync(CreateCourseRequest request, string userId)
    {
        try
        {
            var existingCourseWithSameTitleAndMentor = await _appDbContext.Courses
                .FirstOrDefaultAsync(x => x.Title == request.Title && x.MentorId == userId);
            if(existingCourseWithSameTitleAndMentor != null)
            {
                throw new Exception("You already have a course with the same title!");
            }
            var course = new Course()
            {
                Title = request.Title,
                Description = request.Description,
                AgeGroup = request.AgeGroup,
                DifficultyLevel = request.DifficultyLevel,
                Subject = request.Subject,
                CreatedDate = DateTime.Now,
                StartDate = request.StartDate,
                MentorId = userId
            };
            await _appDbContext.Courses.AddAsync(course);
            await _appDbContext.SaveChangesAsync();
            return await Result.SuccessAsync("Created course successfully!");
        }
        catch (Exception e)
        {
            return await Result.FailAsync(e.Message);
        }
    }

    public async Task<IResult> UpdateCourseAsync(UpdateCourseRequest request, string userId)
    {
        try
        {
            var course = await _appDbContext.Courses.FindAsync(request.Id);
            if (course == null)
            {
                throw new Exception("Course Not Found!");
            }
            if (course.MentorId != userId)
            {
                throw new Exception("Cannot update course that you didn't own!");
            }

            var existingCourseWithSameTitleAndMentor = await _appDbContext.Courses
                .FirstOrDefaultAsync(x => x.Title == request.Title && x.MentorId == userId && x.Id != request.Id);
            if(existingCourseWithSameTitleAndMentor != null)
            {
                throw new Exception("You already have a course with the same title!");
            }
            course.Title = request.Title;
            course.Description = request.Description;
            course.AgeGroup = request.AgeGroup;
            course.DifficultyLevel = request.DifficultyLevel;
            course.Subject = request.Subject;
            course.StartDate = request.StartDate;
            await _appDbContext.SaveChangesAsync();
            return await Result.SuccessAsync("Updated course successfully!");
        }
        catch (Exception e)
        {
            return await Result.FailAsync(e.Message);
        }
    }

    public async Task<IResult> DeleteCourseAsync(string courseId, string userId)
    {
        try
        {
            var course = await _appDbContext.Courses.FindAsync(courseId);
            if (course == null)
            {
                throw new Exception("Course Not Found!");
            }
            if (course.MentorId != userId)
            {
                throw new Exception("Cannot delete course that you didn't own!");
            }
            _appDbContext.Courses.Remove(course);
            await _appDbContext.SaveChangesAsync();
            return await Result.SuccessAsync("Deleted course successfully!");
        }
        catch (Exception e)
        {
            return await Result.FailAsync(e.Message);
        }
    }

    public async Task<IResult> AddCourseModuleAsync(CreateCourseModuleRequest request, string userId)
    {
        try
        {
            var course = await _appDbContext.Courses.FindAsync(request.CourseId);
            if (course == null)
            {
                throw new Exception("Course Not Found!");
            }
            if (course.MentorId != userId)
            {
                throw new Exception("Cannot add module to course that you didn't own!");
            }
            var existingCourseModuleWithSameTitleAndCourse = await _appDbContext.CourseModules
                .FirstOrDefaultAsync(x => x.Title == request.Title && x.CourseId == request.CourseId);
            if(existingCourseModuleWithSameTitleAndCourse != null)
            {
                throw new Exception("You already have a module with the same title in this course!");
            }
            var module = new CourseModule()
            {
                Title = request.Title,
                Description = request.Description,
                CourseId = request.CourseId,
                Order = request.Order,
                StartDate = request.StartDate
            };
            await _appDbContext.CourseModules.AddAsync(module);
            await _appDbContext.SaveChangesAsync();
            return await Result.SuccessAsync("Created module successfully!");
        }
        catch (Exception e)
        {
            return await Result.FailAsync(e.Message);
        }
    }

    public async Task<IResult> UpdateCourseModuleAsync(UpdateCourseModuleRequest request, string userId)
    {
        try
        {
            var module = await _appDbContext.CourseModules.FindAsync(request.Id);
            if (module == null)
            {
                throw new Exception("Module Not Found!");
            }
            var course = await _appDbContext.Courses.FindAsync(module.CourseId);
            if (course == null)
            {
                throw new Exception("Course Not Found!");
            }

            if (course.MentorId != userId)
            {
                throw new Exception("Cannot update module of course that you didn't own!");
            }
            
            var existingCourseModuleWithSameTitleAndCourse = await _appDbContext.CourseModules
                .FirstOrDefaultAsync(x => x.Title == request.Title && x.CourseId == module.CourseId && x.Id != request.Id);
            if(existingCourseModuleWithSameTitleAndCourse != null)
            {
                throw new Exception("You already have a module with the same title in this course!");
            }

            module.Title = request.Title;
            module.Description = request.Description;
            module.Order = request.Order;
            module.StartDate = request.StartDate;
            await _appDbContext.SaveChangesAsync();
            return await Result.SuccessAsync("Updated module successfully!");
        }
        catch (Exception e)
        {
            return await Result.FailAsync(e.Message);
        }
    }

    public async Task<IResult> DeleteCourseModuleAsync(string moduleId, string userId)
    {
        try
        {
            var module = await _appDbContext.CourseModules.FindAsync(moduleId);
            if (module == null)
            {
                throw new Exception("Module Not Found!");
            }
            var course = await _appDbContext.Courses.FindAsync(module.CourseId);
            if (course == null)
            {
                throw new Exception("Course Not Found!");
            }
            if (course.MentorId != userId)
            {
                throw new Exception("Cannot delete module of course that you didn't own!");
            }
            _appDbContext.CourseModules.Remove(module);
            var nextModules = await _appDbContext.CourseModules
                .Where(x => x.CourseId == module.CourseId && x.Order > module.Order)
                .ToListAsync();
            foreach (var courseModule in nextModules)
            {
                courseModule.Order--;
            }
            await _appDbContext.SaveChangesAsync();
            return await Result.SuccessAsync("Deleted module successfully!");
        }
        catch (Exception e)
        {
            return await Result.FailAsync(e.Message);
        }
    }
}