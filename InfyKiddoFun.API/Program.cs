using InfyKiddoFun.Application.Features;
using InfyKiddoFun.Application.Interfaces;
using InfyKiddoFun.Domain.Entities;
using InfyKiddoFun.Infrastructure;
using Microsoft.EntityFrameworkCore;

// Create WebApplicationBuilder object
var builder = WebApplication.CreateBuilder(args);

// Register all services to the IoC container
var connectionString = builder.Configuration.GetConnectionString("Connection1");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddIdentityCore<ParentUser>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddIdentityCore<MentorUser>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddIdentityCore<StudentUser>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddControllers();
builder.Services.AddTransient<ICourseService, CourseService>();
builder.Services.AddTransient<IParentUserService, ParentUserService>();
builder.Services.AddSwaggerGen();

// Build WebApplication object
var app = builder.Build();

// Configure middlewares
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "InfyKiddoFun");
    options.DisplayRequestDuration();
    options.RoutePrefix = "swagger";
});

// Run the WebApplication
app.Run();
