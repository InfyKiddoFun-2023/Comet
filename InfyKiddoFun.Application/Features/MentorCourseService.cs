using InfyKiddoFun.Application.Extensions;
using InfyKiddoFun.Application.Interfaces;
using InfyKiddoFun.Application.Models.Courses;
using InfyKiddoFun.Application.Specifications;
using InfyKiddoFun.Domain.Entities;
using InfyKiddoFun.Domain.Wrapper;
using InfyKiddoFun.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace InfyKiddoFun.Application.Features;

public class MentorCourseService : IMentorCourseService
{
    private readonly AppDbContext _appDbContext;
    private readonly ICurrentUserService _currentUserService;

    public MentorCourseService(AppDbContext appDbContext, ICurrentUserService currentUserService)
    {
        _appDbContext = appDbContext;
        _currentUserService = currentUserService;
    }
    
    public async Task<PaginatedResult<CourseResponse>> GetCoursesAsync(int pageNumber, int pageSize, string searchQuery)
    {
        try
        {
            return await _appDbContext.Courses
                .Include(x => x.Mentor)
                .Include(x => x.Enrollments)
                .Where(x => x.MentorId == _currentUserService.UserId)
                .Specify(new CourseSearchFilterSpecification(searchQuery))
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

    public async Task<IResult<string>> AddCourseAsync(CreateCourseRequest request)
    {
        try
        {
            var existingCourseWithSameTitleAndMentor = await _appDbContext.Courses
                .FirstOrDefaultAsync(x => x.Title == request.Title && x.MentorId == _currentUserService.UserId);
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
                MentorId = _currentUserService.UserId
            };
            await _appDbContext.Courses.AddAsync(course);
            await _appDbContext.SaveChangesAsync();
            course = await _appDbContext.Courses.FirstOrDefaultAsync(x => x.Title == request.Title && x.MentorId == _currentUserService.UserId);
            return await Result<string>.SuccessAsync(message: "Created course successfully!", data: course.Id);
        }
        catch (Exception e)
        {
            return await Result<string>.FailAsync(e.Message);
        }
    }

    public async Task<IResult> UpdateCourseAsync(UpdateCourseRequest request)
    {
        try
        {
            var course = await _appDbContext.Courses.FindAsync(request.Id);
            if (course == null)
            {
                throw new Exception("Course Not Found!");
            }
            if (course.MentorId != _currentUserService.UserId)
            {
                throw new Exception("Cannot update course that you didn't own!");
            }

            var existingCourseWithSameTitleAndMentor = await _appDbContext.Courses
                .FirstOrDefaultAsync(x => x.Title == request.Title && x.MentorId == _currentUserService.UserId && x.Id != request.Id);
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

    public async Task<IResult> DeleteCourseAsync(string courseId)
    {
        try
        {
            var course = await _appDbContext.Courses.FindAsync(courseId);
            if (course == null)
            {
                throw new Exception("Course Not Found!");
            }
            if (course.MentorId != _currentUserService.UserId)
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

    public async Task<IResult> AddCourseModuleAsync(CreateCourseModuleRequest request)
    {
        try
        {
            var course = await _appDbContext.Courses.FindAsync(request.CourseId);
            if (course == null)
            {
                throw new Exception("Course Not Found!");
            }
            if (course.MentorId != _currentUserService.UserId)
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

    public async Task<IResult> UpdateCourseModuleAsync(UpdateCourseModuleRequest request)
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

            if (course.MentorId != _currentUserService.UserId)
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

    public async Task<IResult> DeleteCourseModuleAsync(string moduleId)
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
            if (course.MentorId != _currentUserService.UserId)
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

    public async Task<IResult> AddCourseModuleMaterialAsync(CreateCourseModuleMaterialRequest request)
    {
        try
        {
            var module = await _appDbContext.CourseModules.FindAsync(request.ModuleId);
            if (module == null)
            {
                throw new Exception("Module Not Found!");
            }
            var course = await _appDbContext.Courses.FindAsync(module.CourseId);
            if (course == null)
            {
                throw new Exception("Course Not Found!");
            }
            if (course.MentorId != _currentUserService.UserId)
            {
                throw new Exception("Cannot add material to module of course that you didn't own!");
            }
            var existingCourseModuleMaterialWithSameTitleAndModule = await _appDbContext.CourseModuleMaterials
                .FirstOrDefaultAsync(x => x.Title == request.Title && x.ModuleId == request.ModuleId);
            if(existingCourseModuleMaterialWithSameTitleAndModule != null)
            {
                throw new Exception("You already have a material with the same title in this module!");
            }
            var existingCourseModuleMaterialWithSameOrderAndModule = await _appDbContext.CourseModuleMaterials
                .FirstOrDefaultAsync(x => x.Order == request.Order && x.ModuleId == request.ModuleId);
            if(existingCourseModuleMaterialWithSameOrderAndModule != null)
            {
                throw new Exception("You already have a material with the same order number in this module!");
            }
            var material = new CourseModuleMaterial()
            {
                Title = request.Title,
                ModuleId = request.ModuleId,
                Order = request.Order,
                Link = request.Link,
                MaterialType = request.MaterialType
            };
            module.Materials.Add(material);
            _appDbContext.CourseModules.Update(module);
            await _appDbContext.SaveChangesAsync();
            return await Result.SuccessAsync("Created material successfully!");
        }
        catch (Exception e)
        {
            return await Result.FailAsync(e.Message);
        }
    }

    public async Task<IResult> UpdateCourseModuleMaterialAsync(UpdateCourseModuleMaterialRequest request)
    {
        try
        {
            var material = await _appDbContext.CourseModuleMaterials.FindAsync(request.Id);
            if (material == null)
            {
                throw new Exception("Material Not Found!");
            }
            var module = await _appDbContext.CourseModules.FindAsync(material.ModuleId);
            if (module == null)
            {
                throw new Exception("Module Not Found!");
            }
            var course = await _appDbContext.Courses.FindAsync(module.CourseId);
            if (course == null)
            {
                throw new Exception("Course Not Found!");
            }
            if (course.MentorId != _currentUserService.UserId)
            {
                throw new Exception("Cannot update material of module of course that you didn't own!");
            }
            var existingCourseModuleMaterialWithSameTitleAndModule = await _appDbContext.CourseModuleMaterials
                .FirstOrDefaultAsync(x => x.Title == request.Title && x.ModuleId == material.ModuleId && x.Id != request.Id);
            if(existingCourseModuleMaterialWithSameTitleAndModule != null)
            {
                throw new Exception("You already have a material with the same title in this module!");
            }
            var existingCourseModuleMaterialWithSameOrderAndModule = await _appDbContext.CourseModuleMaterials
                .FirstOrDefaultAsync(x => x.Order == request.Order && x.ModuleId == material.ModuleId && x.Id != request.Id);
            if(existingCourseModuleMaterialWithSameOrderAndModule != null)
            {
                throw new Exception("You already have a material with the same order number in this module!");
            }
            material.Title = request.Title;
            material.Order = request.Order;
            material.Link = request.Link;
            material.MaterialType = request.MaterialType;
            _appDbContext.CourseModuleMaterials.Update(material);
            await _appDbContext.SaveChangesAsync();
            return await Result.SuccessAsync("Updated material successfully!");
        }
        catch (Exception e)
        {
            return await Result.FailAsync(e.Message);
        }
    }

    public async Task<IResult> DeleteCourseModuleMaterialAsync(string materialId)
    {
        try
        {
            var material = await _appDbContext.CourseModuleMaterials.FindAsync(materialId);
            if (material == null)
            {
                throw new Exception("Material Not Found!");
            }
            var module = await _appDbContext.CourseModules.FindAsync(material.ModuleId);
            if (module == null)
            {
                throw new Exception("Module Not Found!");
            }
            var course = await _appDbContext.Courses.FindAsync(module.CourseId);
            if (course == null)
            {
                throw new Exception("Course Not Found!");
            }
            if (course.MentorId != _currentUserService.UserId)
            {
                throw new Exception("Cannot delete material of module of course that you didn't own!");
            }
            _appDbContext.CourseModuleMaterials.Remove(material);
            var nextMaterials = await _appDbContext.CourseModuleMaterials
                .Where(x => x.ModuleId == material.ModuleId && x.Order > material.Order)
                .ToListAsync();
            foreach (var courseModuleMaterial in nextMaterials)
            {
                courseModuleMaterial.Order--;
            }
            await _appDbContext.SaveChangesAsync();
            return await Result.SuccessAsync("Deleted material successfully!");
        }
        catch (Exception e)
        {
            return await Result.FailAsync(e.Message);
        }
    }

    public async Task<IResult> AddCourseModuleQuizAsync(CreateCourseModuleQuizRequest request)
    {
        try
        {
            var module = await _appDbContext.CourseModules.FindAsync(request.ModuleId);
            if (module == null)
            {
                throw new Exception("Module Not Found!");
            }
            var course = await _appDbContext.Courses.FindAsync(module.CourseId);
            if (course == null)
            {
                throw new Exception("Course Not Found!");
            }
            if (course.MentorId != _currentUserService.UserId)
            {
                throw new Exception("Cannot add quiz to module of course that you didn't own!");
            }
            var existingQuizInSameModule = await _appDbContext.CourseModuleQuizzes
                .FirstOrDefaultAsync(x => x.ModuleId == request.ModuleId);
            if(existingQuizInSameModule != null)
            {
                throw new Exception("You already have a quiz in this module!");
            }
            var quiz = new CourseModuleQuiz()
            {
                Title = request.Title,
                PassPercentage = request.PassPercentage,
            };
            foreach (var question in request.Questions)
            {
                var quizQuestion = new CourseModuleQuizQuestion()
                {
                    Question = question.Question,
                    Options = question.Options,
                    CorrectOption = question.CorrectOptions,
                    QuizId = quiz.Id
                };
                quiz.Questions.Add(quizQuestion);
            }
            module.Quiz = quiz;
            _appDbContext.CourseModules.Update(module);
            await _appDbContext.SaveChangesAsync();
            return await Result.SuccessAsync("Created quiz successfully!");
        }
        catch (Exception e)
        {
            return await Result.FailAsync(e.Message);
        }
    }

    public async Task<IResult> UpdateCourseModuleQuizAsync(UpdateCourseModuleQuizRequest request)
    {
        try
        {
            var quiz = await _appDbContext.CourseModuleQuizzes.FindAsync(request.Id);
            if (quiz == null)
            {
                throw new Exception("Quiz Not Found!");
            }
            var module = await _appDbContext.CourseModules.FindAsync(quiz.ModuleId);
            if (module == null)
            {
                throw new Exception("Module Not Found!");
            }
            var course = await _appDbContext.Courses.FindAsync(module.CourseId);
            if (course == null)
            {
                throw new Exception("Course Not Found!");
            }
            if (course.MentorId != _currentUserService.UserId)
            {
                throw new Exception("Cannot update quiz of module of course that you didn't own!");
            }
            quiz.Title = request.Title;
            quiz.PassPercentage = request.PassPercentage;
            quiz.Questions.Clear();
            foreach (var question in request.Questions)
            {
                quiz.Questions.Add(new CourseModuleQuizQuestion()
                {
                    Question = question.Question,
                    Options = question.Options,
                    CorrectOption = question.CorrectOptions,
                });
            }
            _appDbContext.CourseModuleQuizzes.Update(quiz);
            await _appDbContext.SaveChangesAsync();
            return await Result.SuccessAsync("Updated quiz successfully!");
        }
        catch (Exception e)
        {
            return await Result.FailAsync(e.Message);
        }
    }

    public async Task<IResult> DeleteCourseModuleQuizAsync(string quizId)
    {
        try
        {
            var quiz = await _appDbContext.CourseModuleQuizzes.FindAsync(quizId);
            if (quiz == null)
            {
                throw new Exception("Quiz Not Found!");
            }
            var module = await _appDbContext.CourseModules.FindAsync(quiz.ModuleId);
            if (module == null)
            {
                throw new Exception("Module Not Found!");
            }
            var course = await _appDbContext.Courses.FindAsync(module.CourseId);
            if (course == null)
            {
                throw new Exception("Course Not Found!");
            }
            if (course.MentorId != _currentUserService.UserId)
            {
                throw new Exception("Cannot delete quiz of module of course that you didn't own!");
            }
            module.Quiz = null;
            _appDbContext.CourseModuleQuizzes.Remove(quiz);
            await _appDbContext.SaveChangesAsync();
            return await Result.SuccessAsync("Deleted quiz successfully!");
        }
        catch (Exception e)
        {
            return await Result.FailAsync(e.Message);
        }
    }
}