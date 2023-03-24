using InfyKiddoFun.Application.Interfaces;
using InfyKiddoFun.Domain.Entities;
using InfyKiddoFun.Domain.Wrapper;
using InfyKiddoFun.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace InfyKiddoFun.Application.Features;

public class StudentCourseService : IStudentCourseService
{
    private readonly AppDbContext _appDbContext;
    private readonly ICurrentUserService _currentUserService;

    public StudentCourseService(AppDbContext appDbContext, ICurrentUserService currentUserService)
    {
        _appDbContext = appDbContext;
        _currentUserService = currentUserService;
    }

    public async Task<IResult<bool>> IsEnrolledInCourseAsync(string courseId)
    {
        try
        {
            var course = await _appDbContext.Courses.FindAsync(courseId);
            if (course == null)
            {
                throw new Exception("Course Not Found!");
            }

            var enrollment = await _appDbContext.Enrollments
                .FirstOrDefaultAsync(x => x.CourseId == courseId && x.StudentId == _currentUserService.UserId);
            if (enrollment == null)
            {
                return await Result<bool>.SuccessAsync(false);
            }

            return await Result<bool>.SuccessAsync(true);
        }
        catch (Exception e)
        {
            return await Result<bool>.FailAsync(e.Message);
        }
    }

    public async Task<IResult> EnrollCourseAsync(string courseId)
    {
        try
        {
            var course = await _appDbContext.Courses.FindAsync(courseId);
            if (course == null)
            {
                throw new Exception("Course Not Found!");
            }
            
            var existingEnrollment = await _appDbContext.Enrollments
                .FirstOrDefaultAsync(x => x.CourseId == courseId && x.StudentId == _currentUserService.UserId);
            if(existingEnrollment != null)
            {
                throw new Exception("Already Enrolled!");
            }

            var enrollment = await _appDbContext.Enrollments
                .FirstOrDefaultAsync(x => x.CourseId == courseId && x.StudentId == _currentUserService.UserId);
            if (enrollment != null)
            {
                throw new Exception("Already Enrolled!");
            }

            await _appDbContext.Enrollments.AddAsync(new CourseEnrollment
            {
                CourseId = courseId,
                StudentId = _currentUserService.UserId,
                EnrollDate = DateTime.Now
            });
            
            var courseProgress = new CourseProgress
            {
                CourseId = courseId,
                StudentId = _currentUserService.UserId,
                LastUpdated = DateTime.Now
            };
            foreach (var module in course.Modules)
            {
                courseProgress.CourseModules.Add(new CourseModuleProgress
                {
                    CourseModuleId = module.Id,
                    Completed = false,
                    LastUpdated = DateTime.Now
                });
            }
            await _appDbContext.CourseProgresses.AddAsync(courseProgress);

            await _appDbContext.SaveChangesAsync();

            return await Result.SuccessAsync();
        }
        catch (Exception e)
        {
            return await Result.FailAsync(e.Message);
        }
    }

    public async Task<IResult> MarkCourseModuleCompletedAsync(string courseModuleId)
    {
        try
        {
            var courseModule = await _appDbContext.CourseModules.FindAsync(courseModuleId);
            if (courseModule == null)
            {
                throw new Exception("Course Module Not Found!");
            }

            var enrollment = await _appDbContext.Enrollments
                .FirstOrDefaultAsync(x => x.CourseId == courseModule.CourseId && x.StudentId == _currentUserService.UserId);
            if (enrollment == null)
            {
                throw new Exception("Not Enrolled!");
            }

            var courseProgress = await _appDbContext.CourseProgresses
                .Include(x => x.CourseModules)
                .FirstOrDefaultAsync(x => x.CourseId == courseModule.CourseId && x.StudentId == _currentUserService.UserId);
            if (courseProgress == null)
            {
                throw new Exception("Course Progress Not Found!");
            }

            var courseModuleProgress = await _appDbContext.CourseModuleProgresses
                .FirstOrDefaultAsync(x => x.CourseModuleId == courseModuleId && x.CourseProgressId == courseProgress.Id);
            if (courseModuleProgress == null)
            {
                throw new Exception("Course Module Progress Not Found!");
            }

            courseModuleProgress.Completed = true;
            courseModuleProgress.CompletedOn = DateTime.Now;
            courseModuleProgress.LastUpdated = DateTime.Now;
            
            if(courseProgress.CourseModules.All(x => x.Completed))
            {
                courseProgress.Completed = true;
                courseProgress.CompletedOn = DateTime.Now;
            }

            await _appDbContext.SaveChangesAsync();

            return await Result.SuccessAsync(courseProgress.Completed ? "Course Completed!" : "Course Module Completed!"); 
        }
        catch (Exception e)
        {
            return await Result.FailAsync(e.Message);
        }
    }
}