using InfyKiddoFun.Infrastructure;
using Microsoft.EntityFrameworkCore;

// Create WebApplicationBuilder object
var builder = WebApplication.CreateBuilder(args);

// Register all services to the IoC container
var connectionString = builder.Configuration.GetConnectionString("Connection1");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

// Build WebApplication object
var app = builder.Build();

// Configure middlewares
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

// Run the WebApplication
app.Run();
