namespace InfyKiddoFun.Domain.Entities;

public class Enrollment
{
    public string Id { get; set; }
    public string CourseId { get; set; }
    public string StudentId { get; set; }


    public Course Course { get; set; }
    public StudentUser Student { get; set; }
}