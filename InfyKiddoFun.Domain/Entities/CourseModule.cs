﻿namespace InfyKiddoFun.Domain.Entities;

public class CourseModule
{
    public string Id { get; set; }
    public string Title { get; set; }
    
    public Course Course { get; set; }
    public string CourseId { get; set; }
    
    public string Description { get; set; }
}