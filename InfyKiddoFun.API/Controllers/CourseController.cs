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