﻿using InfyKiddoFun.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InfyKiddoFun.Infrastructure
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
           
        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentUser> StudentUsers { get; set; }
        public DbSet<MentorUser> MentorUsers { get; set; }
        public DbSet<ParentUser> ParentUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Course>().Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}