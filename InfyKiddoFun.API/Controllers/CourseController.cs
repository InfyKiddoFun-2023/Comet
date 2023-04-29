using InfyKiddoFun.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InfyKiddoFun.API.Controllers;

[Authorize]
[Route("api/courses")]
[ApiController]
public class CourseController : ControllerBase
{
    private readonly ICourseService _courseService;

    public CourseController(ICourseService courseService)
    {
        _courseService = courseService;
    }
    
    [HttpGet("subject/{subject:int}")]
    public async Task<IActionResult> GetCoursesBySubjectAsync(int pageNumber, int pageSize, int subject, string searchQuery)
    {
        return Ok(await _courseService.GetCoursesBySubjectAsync(pageNumber, pageSize, subject, searchQuery));
    }
    
    [HttpGet("age-group/{ageGroup:int}")]
    public async Task<IActionResult> GetCoursesByAgeGroupAsync(int pageNumber, int pageSize, int ageGroup, string searchQuery)
    {
        return Ok(await _courseService.GetCoursesByAgeGroupAsync(pageNumber, pageSize, ageGroup, searchQuery));
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCoursesAsync(int pageNumber, int pageSize, string searchQuery)
    {
        return Ok(await _courseService.GetCoursesAsync(pageNumber, pageSize, searchQuery));
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCourseAsync(string id)
    {
        return Ok(await _courseService.GetCourseAsync(id));
    }
}