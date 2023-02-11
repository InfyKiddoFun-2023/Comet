﻿using InfyKiddoFun.Application.Interfaces;
using InfyKiddoFun.Application.Models;
using InfyKiddoFun.Domain.Entities;
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

    public async Task<List<CourseResponseModel>> GetAllCourse(int pageNumber, int pageSize, string searchString)
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
        return courses;
    }

    public CourseResponseModel GetById(string id)
    {
        var course = _appDbContext.Courses.Find(id);
        if (course == null) return null;
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
        return model;
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

    public void DeleteCourse(string id)
    {
        var existingCourse = _appDbContext.Courses.Find(id);
        if (existingCourse == null) return;
        _appDbContext.Courses.Remove(existingCourse);
        _appDbContext.SaveChanges();
    }
}