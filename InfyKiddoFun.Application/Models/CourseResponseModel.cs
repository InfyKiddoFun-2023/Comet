using InfyKiddoFun.Domain.Enums;

namespace InfyKiddoFun.Application.Models;

public class CourseResponseModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Duration { get; set; }

    public AgeGroup AgeGroup { get; set; }

    public DifficultyLevel DifficultyLevel { get; set; }

    public SpecificStream SpecificStream { get; set; }
}