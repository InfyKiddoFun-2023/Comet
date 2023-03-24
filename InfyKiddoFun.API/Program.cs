using InfyKiddoFun.API.Extensions;
using Microsoft.Extensions.FileProviders;

// Create WebApplicationBuilder object
var builder = WebApplication.CreateBuilder(args);

// Register all services to the IoC container
builder.Services
    .AddConfigurations(builder.Configuration)
    .AddCurrentUserService()
    .AddDatabaseWithIdentity(builder.Configuration.GetConnectionString("DefaultConnection"))
    .AddJwtAuthentication(builder.Configuration.GetTokenConfiguration())
    .AddPolicyAuthorization()
    .AddApplicationFeatures()
    .AddSwagger()
    .AddControllers();

// Build WebApplication object
var app = builder.Build();

// Configure middlewares
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Files")),
    RequestPath = new PathString("/Files")
});
app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint($"/swagger/v1/swagger.json", "Comet V1");
    options.RoutePrefix = "swagger";
});

// Run the WebApplication
app.Run();
