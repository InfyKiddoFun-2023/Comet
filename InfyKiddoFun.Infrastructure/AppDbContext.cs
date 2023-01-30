using Microsoft.EntityFrameworkCore;

namespace InfyKiddoFun.Infrastructure
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
           
        }
    }
}