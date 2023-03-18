using System.Net;
using System.Text;
using System.Text.Json;
using InfyKiddoFun.Application.Features;
using InfyKiddoFun.Application.Interfaces;
using InfyKiddoFun.Domain.Configurations;
using InfyKiddoFun.Domain.Constants;
using InfyKiddoFun.Domain.Entities;
using InfyKiddoFun.Domain.Wrapper;
using InfyKiddoFun.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace InfyKiddoFun.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<TokenConfiguration>(configuration.GetSection(nameof(TokenConfiguration)));
        return services;
    }
    
    public static IServiceCollection AddDatabaseWithIdentity(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
        services.AddIdentityCore<ParentUser>().AddEntityFrameworkStores<AppDbContext>();
        services.AddIdentityCore<MentorUser>().AddEntityFrameworkStores<AppDbContext>();
        services.AddIdentityCore<StudentUser>().AddEntityFrameworkStores<AppDbContext>();
        return services;
    }

    public static IServiceCollection AddApplicationFeatures(this IServiceCollection services)
    {
        services.AddTransient<IParentUserService, ParentUserService>();
        services.AddTransient<IStudentUserService, StudentUserService>();
        services.AddTransient<IMentorUserService, MentorUserService>();
        services.AddTransient<IMentorCourseService, MentorCourseService>();
        services.AddTransient<IStudentCourseService, StudentCourseService>();
        services.AddTransient<ICourseService, CourseService>();
        return services;
    }
    
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, TokenConfiguration tokenConfiguration)
    {
        var key = Encoding.ASCII.GetBytes(tokenConfiguration.Secret);
        services
            .AddAuthentication(authentication =>
            {
                authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(bearer =>
            {
                bearer.RequireHttpsMetadata = false;
                bearer.SaveToken = true;
                bearer.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RoleClaimType = ApplicationClaimTypes.Role,
                    ClockSkew = TimeSpan.Zero
                };
                bearer.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = c =>
                    {
                        if (c.Exception is SecurityTokenExpiredException)
                        {
                            c.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            c.Response.ContentType = "application/json";
                            var result = JsonSerializer.Serialize(Result.Fail("The Token is expired."));
                            return c.Response.WriteAsync(result);
                        }
                        else
                        {
                            c.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            c.Response.ContentType = "application/json";
                            var result = JsonSerializer.Serialize(Result.Fail("An unhandled error has occurred."));
                            return c.Response.WriteAsync(result);
                        }
                    },
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        if (context.Response.HasStarted) 
                            return Task.CompletedTask;
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        context.Response.ContentType = "application/json";
                        var result = JsonSerializer.Serialize(Result.Fail("You are not Authorized."));
                        return context.Response.WriteAsync(result);

                    },
                    OnForbidden = context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        context.Response.ContentType = "application/json";
                        var result = JsonSerializer.Serialize(Result.Fail("You are not authorized to access this resource."));
                        return context.Response.WriteAsync(result);
                    },
                };
            });
        return services;
    }
    
    public static IServiceCollection AddPolicyAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(Policies.Mentor, policy => policy.RequireRole(Roles.Mentor));
            options.AddPolicy(Policies.Student, policy => policy.RequireRole(Roles.Student));
            options.AddPolicy(Policies.Parent, policy => policy.RequireRole(Roles.Parent));
        });
        return services;
    }
    
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Description = "Input your Bearer token in this format - Bearer {your token here} to access this API",
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer",
                        },
                        Scheme = "Bearer",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    }, new List<string>()
                },
            });
        });
        return services;
    }
}