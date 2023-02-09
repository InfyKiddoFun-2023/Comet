using InfyKiddoFun.Application.Interfaces;
using InfyKiddoFun.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace InfyKiddoFun.API.Controllers;

[Route("api/courses")]
[ApiController]
public class CourseController : ControllerBase
{
    private readonly ICourseService _courseService;

    public CourseController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpPost("add")]
    public IActionResult AddCourse(AddEditCourseModel model)
    {
        _courseService.AddCourse(model);
        return Ok();
    }
    
}