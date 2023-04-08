using InfyKiddoFun.Application.Interfaces;
using InfyKiddoFun.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InfyKiddoFun.API.Controllers;

[Authorize(Roles = Roles.Student)]
[Route("api/student/courses")]
[ApiController]
public class StudentCourseController : ControllerBase
{
    private readonly IStudentCourseService _studentCourseService;

    public StudentCourseController(IStudentCourseService studentCourseService)
    {
        _studentCourseService = studentCourseService;
    }

    [HttpGet]
    public async Task<IActionResult> GetEnrolledCoursesAsync(int pageNumber, int pageSize, string searchString)
    {
        return Ok(await _studentCourseService.GetEnrolledCoursesAsync(pageNumber, pageSize, searchString));
    }
    
    [HttpGet("enrollment/{courseId}")]
    public async Task<IActionResult> IsEnrolledInCourseAsync(string courseId)
    {
        return Ok(await _studentCourseService.IsEnrolledInCourseAsync(courseId));
    }
    
    [HttpPost("{courseId}/enroll")]
    public async Task<IActionResult> EnrollCourseAsync(string courseId)
    {
        return Ok(await _studentCourseService.EnrollCourseAsync(courseId));
    }
    
    [HttpPost("{courseModuleId}/mark-completed")]
    public async Task<IActionResult> MarkCourseModuleCompletedAsync(string courseModuleId)
    {
        return Ok(await _studentCourseService.MarkCourseModuleCompletedAsync(courseModuleId));
    }
}