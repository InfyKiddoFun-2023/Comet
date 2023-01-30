using InfyKiddoFun.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InfyKiddoFun.Infrastructure
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
           
        }
        public DbSet<Course> Courses { get; set; }


    }
}