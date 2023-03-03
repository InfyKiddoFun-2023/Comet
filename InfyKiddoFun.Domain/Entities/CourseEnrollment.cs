namespace InfyKiddoFun.Domain.Entities;

public class CourseEnrollment
{
    public string Id { get; set; }
    
    public StudentUser Student { get; set; }
    public string StudentId { get; set; }
    
    public DateTime EnrollDate { get; set; }

    public Course Course { get; set; }
    public string CourseId { get; set; }
}