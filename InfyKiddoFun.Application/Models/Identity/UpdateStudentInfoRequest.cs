using InfyKiddoFun.Domain.Enums;

namespace InfyKiddoFun.Application.Models.Identity;

public class UpdateStudentInfoRequest
{
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string AboutMe { get; set; }
    public AgeGroup AgeGroup { get; set; }
    public List<Subject> Subjects { get; set; }
}