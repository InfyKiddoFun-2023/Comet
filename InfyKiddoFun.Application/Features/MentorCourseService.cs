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
}