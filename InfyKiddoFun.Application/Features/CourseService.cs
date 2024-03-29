﻿using InfyKiddoFun.Application.Extensions;
using InfyKiddoFun.Application.Interfaces;
using InfyKiddoFun.Application.Models.Courses;
using InfyKiddoFun.Application.Specifications;
using InfyKiddoFun.Domain.Enums;
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

    public async Task<PaginatedResult<CourseResponse>> GetCoursesBySubjectAsync(int pageNumber, int pageSize, int subject, string searchQuery)
    {
        try
        {
            var parseSub = Enum.TryParse<Subject>(subject.ToString(), out var sub);
            if (!parseSub)
            {
                return PaginatedResult<CourseResponse>.Failure(new List<string> { "Invalid subject." });
            }
            return await _appDbContext.Courses
                .Include(x => x.Mentor)
                .Include(x => x.Enrollments)
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
            return PaginatedResult<CourseResponse>.Failure(new List<string> { e.Message });
        }
    }

    public async Task<PaginatedResult<CourseResponse>> GetCoursesByAgeGroupAsync(int pageNumber, int pageSize, int ageGroup, string searchQuery)
    {
        try
        {
            var parseAge = Enum.TryParse<AgeGroup>(ageGroup.ToString(), out var age);
            if (!parseAge)
            {
                return PaginatedResult<CourseResponse>.Failure(new List<string> { "Invalid age group." });
            }
            return await _appDbContext.Courses
                .Include(x => x.Mentor)
                .Include(x => x.Enrollments)
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
            return PaginatedResult<CourseResponse>.Failure(new List<string> { e.Message });
        }
    }

    public async Task<PaginatedResult<CourseResponse>> GetCoursesAsync(int pageNumber, int pageSize, string searchQuery, Subject? subject, AgeGroup? ageGroup)
    {
        try
        {
            return await _appDbContext.Courses
                .Include(x => x.Mentor)
                .Include(x => x.Enrollments)
                .Specify(new CourseSearchFilterSpecification(searchQuery))
                .Specify(new CourseSubjectFilterSpecification(subject))
                .Specify(new CourseAgeGroupFilterSpecification(ageGroup))
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
    
    public async Task<IResult<CourseFullResponse>> GetCourseAsync(string courseId)
    {
        try
        {
            var course = await _appDbContext.Courses
                .Include(x => x.Mentor)
                .Include(x => x.Enrollments)
                .FirstOrDefaultAsync(x => x.Id == courseId);
            if (course == null)
            {
                throw new Exception("Course Not Found!");
            }
            var courseFullResponse = new CourseFullResponse
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                MentorName = course.Mentor.FullName,
                MentorId = course.MentorId,
                AgeGroup = course.AgeGroup.ToDescriptionString(),
                Stream = course.Subject.ToDescriptionString(),
                DifficultyLevel = course.DifficultyLevel.ToDescriptionString(),
                Enrollments = course.Enrollments.Count,
                StartDate = course.StartDate,
                CreatedDate = course.CreatedDate,
            };
            var modules = await _appDbContext.CourseModules
                .Include(x => x.Materials)
                .Include(x => x.Quiz)
                .ThenInclude(x => x.Questions)
                .Where(x => x.CourseId == courseId)
                .OrderBy(x => x.Order)
                .Select(x => new CourseModuleResponse
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Order = x.Order,
                    Materials = x.Materials.Select(y => new CourseModuleMaterialResponse
                    {
                        Id = y.Id,
                        MaterialType = y.MaterialType.ToDescriptionString(),
                        Link = y.Link,
                    }).ToList(),
                    QuizTitle = x.Quiz.Title,
                    QuizQuestions = x.Quiz.Questions.Count,
                }).ToListAsync();
            courseFullResponse.Modules = modules;
            return await Result<CourseFullResponse>.SuccessAsync(courseFullResponse);
        }
        catch (Exception e)
        {
            return await Result<CourseFullResponse>.FailAsync(e.Message);
        }
    }
}