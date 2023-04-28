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

    public async Task<IResult<QuizAttemptResponse>> AttemptQuizAsync(QuizAttemptRequest request)
    {
        try
        {
            var courseModule = await _appDbContext.CourseModules.FindAsync(request.CourseModuleId);
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
                .FirstOrDefaultAsync(x => x.CourseModuleId == request.CourseModuleId && x.CourseProgressId == courseProgress.Id);
            if (courseModuleProgress == null)
            {
                throw new Exception("Course Module Progress Not Found!");
            }

            var quiz = await _appDbContext.CourseModuleQuizzes.FindAsync(request.QuizId);
            if (quiz == null)
            {
                throw new Exception("Quiz Not Found!");
            }
            
            var quizAttempt = await _appDbContext.CourseModuleQuizAttempts
                .Include(x => x.Quiz)
                .FirstOrDefaultAsync(x => x.QuizId == request.QuizId && x.UserId == _currentUserService.UserId);
            quizAttempt = quizAttempt ?? new CourseModuleQuizAttempt
            {
                QuizId = request.QuizId,
                UserId = _currentUserService.UserId,
                //AttemptedOn = DateTime.Now,
            };
            await _appDbContext.CourseModuleQuizAttempts.AddAsync(quizAttempt);
            await _appDbContext.SaveChangesAsync();
            return await Result<QuizAttemptResponse>.SuccessAsync(new QuizAttemptResponse
            {
                QuizId = quiz.Id,
                QuizTitle = quiz.Title,
                Questions = quiz.Questions.Select(x => new QuizQuestionAttemptResponse
                {
                    Question = x.Question,
                }).ToList()
            });
        }
        catch (Exception e)
        {
            return await Result<QuizAttemptResponse>.FailAsync(e.Message);
        }
    }

    public async Task<IResult<QuizAttemptResponse>> GetQuizAttemptResultAsync(string quizId)
    {
        throw new NotImplementedException();
    }

    public async Task<PaginatedResult<CourseResponse>> GetEnrolledCoursesAsync(int pageNumber, int pageSize, string searchString)
    {
        try
        {
            return await _appDbContext.Courses
                .Include(x => x.Mentor)
                .Include(x => x.Enrollments)
                .Where(x => x.Enrollments.Any(y => y.StudentId == _currentUserService.UserId))
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
            return PaginatedResult<CourseResponse>.Failure(new List<string> { e.Message });
        }
    }
}