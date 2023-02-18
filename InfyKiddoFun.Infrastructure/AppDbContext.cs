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
    
    public DbSet<StudentUser> StudentUsers { get; set; }
    public DbSet<ParentUser> ParentUsers { get; set; }
    public DbSet<MentorUser> MentorUsers { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<StudentUser>()
            .Property(x => x.SpecificStream)
            .HasColumnName("SpecificStream");
        
        modelBuilder.Entity<MentorUser>()
            .Property(x => x.SpecificStream)
            .HasColumnName("SpecificStream");

        modelBuilder.Entity<AppUser>(entity =>
        {
            entity.ToTable("AppUsers").Property(x => x.Id).ValueGeneratedOnAdd();
            entity.HasDiscriminator<string>("UserType")
                .HasValue<ParentUser>("Parent")
                .HasValue<StudentUser>("Student")
                .HasValue<MentorUser>("Mentor");
        });
        
        modelBuilder.Ignore<IdentityRole>();
        modelBuilder.Ignore<IdentityUserClaim<string>>();
        modelBuilder.Ignore<IdentityUserRole<string>>();
        modelBuilder.Ignore<IdentityRoleClaim<string>>();
        modelBuilder.Ignore<IdentityUserLogin<string>>();
        modelBuilder.Ignore<IdentityUserToken<string>>();
        
        modelBuilder.Entity<Course>().Property(x => x.Id).ValueGeneratedOnAdd();
    } 
}