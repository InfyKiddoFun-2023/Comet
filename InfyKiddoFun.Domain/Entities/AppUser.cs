using Microsoft.AspNetCore.Identity;

namespace InfyKiddoFun.Domain.Entities;

public abstract class AppUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public string AboutMe { get; set; }

    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
}