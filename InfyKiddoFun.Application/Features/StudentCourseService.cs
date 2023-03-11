using InfyKiddoFun.Application.Interfaces;
using InfyKiddoFun.Domain.Entities;
using InfyKiddoFun.Domain.Wrapper;
using InfyKiddoFun.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace InfyKiddoFun.Application.Features;

public class StudentCourseService : IStudentCourseService
{
    private readonly AppDbContext _appDbContext;

    public StudentCourseService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<IResult<bool>> IsEnrolledInCourseAsync(string courseId, string userId)
    {
        try
        {
            var course = await _appDbContext.Courses.FindAsync(courseId);
            if (course == null)
            {
                throw new Exception("Course Not Found!");
            }

            var enrollment = await _appDbContext.Enrollments
                .FirstOrDefaultAsync(x => x.CourseId == courseId && x.StudentId == userId);
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

    public async Task<IResult> EnrollCourseAsync(string courseId, string userId)
    {
        try
        {
            var course = await _appDbContext.Courses.FindAsync(courseId);
            if (course == null)
            {
                throw new Exception("Course Not Found!");
            }
            
            var existingEnrollment = await _appDbContext.Enrollments
                .FirstOrDefaultAsync(x => x.CourseId == courseId && x.StudentId == userId);
            if(existingEnrollment != null)
            {
                throw new Exception("Already Enrolled!");
            }

            var enrollment = await _appDbContext.Enrollments
                .FirstOrDefaultAsync(x => x.CourseId == courseId && x.StudentId == userId);
            if (enrollment != null)
            {
                throw new Exception("Already Enrolled!");
            }

            await _appDbContext.Enrollments.AddAsync(new CourseEnrollment
            {
                CourseId = courseId,
                StudentId = userId,
                EnrollDate = DateTime.Now
            });
            await _appDbContext.SaveChangesAsync();

            return await Result.SuccessAsync();
        }
        catch (Exception e)
        {
            return await Result.FailAsync(e.Message);
        }
    }
}