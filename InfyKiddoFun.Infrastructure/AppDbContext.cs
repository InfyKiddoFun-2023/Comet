using InfyKiddoFun.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InfyKiddoFun.Infrastructure;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
    {
           
    }
    public DbSet<Course> Courses { get; set; }

    public DbSet<Enrollment> Enrollments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //modelBuilder.Entity<AppUser>().ToTable("AppUsers");
        modelBuilder.Ignore<IdentityRole>();
        modelBuilder.Ignore<IdentityUserClaim<string>>();
        modelBuilder.Ignore<IdentityUserRole<string>>();
        modelBuilder.Ignore<IdentityRoleClaim<string>>();
        modelBuilder.Ignore<IdentityUserLogin<string>>();
        modelBuilder.Ignore<IdentityUserToken<string>>();
        
        modelBuilder.Entity<Course>().Property(x => x.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<AppUser>().Property(x => x.Id).ValueGeneratedOnAdd();
    } 
}