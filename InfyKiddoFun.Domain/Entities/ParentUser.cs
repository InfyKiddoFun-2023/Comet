namespace InfyKiddoFun.Domain.Entities;

public class ParentUser : AppUser
{
    public string Id { get; set; }
    public string Name { get; set; }

    public int StudentID { get; set; }
    public string MailID { get; set; }

    public int Mobile { get; set; }
}