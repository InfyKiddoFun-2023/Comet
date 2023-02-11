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

    [HttpGet]
    public async Task<IActionResult> GetAllCourses(int pageNumber, int pageSize, string searchString)
    {
        var courses = await _courseService.GetAllCourse(pageNumber, pageSize, searchString);
        return Ok(courses);
    }

    [HttpGet("{id}")]
    public IActionResult GetCourseById(string id)
    {
        var course = _courseService.GetById(id);
        return Ok(course);
    }

    [HttpPost("add")]
    public IActionResult AddCourse(AddEditCourseModel model)
    {
        _courseService.AddCourse(model);
        return Ok();
    }

    [HttpPut("update")]
    public IActionResult UpdateCourse(AddEditCourseModel model)
    {
        _courseService.UpdateCourse(model);
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCourse(string id)
    {
        _courseService.DeleteCourse(id);
        return Ok();
    }
}