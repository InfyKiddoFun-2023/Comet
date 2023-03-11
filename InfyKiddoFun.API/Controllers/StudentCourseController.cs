using System.Security.Claims;
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
    
    [HttpGet("enrollment/{courseId}")]
    public async Task<IActionResult> IsEnrolledInCourseAsync(string courseId)
    {
        return Ok(await _studentCourseService.IsEnrolledInCourseAsync(courseId, User.FindFirstValue(ApplicationClaimTypes.Id)));
    }
    
    [HttpPost("{courseId}/enroll")]
    public async Task<IActionResult> EnrollCourseAsync(string courseId)
    {
        return Ok(await _studentCourseService.EnrollCourseAsync(courseId, User.FindFirstValue(ApplicationClaimTypes.Id)));
    }
}