using InfyKiddoFun.Application.Interfaces;
using InfyKiddoFun.Application.Models.Courses;
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
    public async Task<IActionResult> GetAllCourses(int pageNumber, int pageSize, string searchString)
    {
        var courses = await _courseService.GetAllCourse(pageNumber, pageSize, searchString);
        return Ok(courses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCourseById(string id)
    {
        var course = await _courseService.GetById(id);
        return Ok(course);
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddCourse(AddEditCourseModel model)
    {
        return Ok(await _courseService.AddCourse(model));
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateCourse(AddEditCourseModel model)
    {
        
        return Ok(await _courseService.UpdateCourse(model));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse(string id)
    {
        return Ok(await _courseService.DeleteCourse(id));
    }
}