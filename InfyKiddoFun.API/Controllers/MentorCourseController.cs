using System.Security.Claims;
using InfyKiddoFun.Application.Interfaces;
using InfyKiddoFun.Application.Models.Courses;
using InfyKiddoFun.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InfyKiddoFun.API.Controllers;

[Authorize]
[Route("api/mentor/courses")]
[ApiController]
public class MentorCourseController : ControllerBase
{
    private readonly IMentorCourseService _mentorCourseService;

    public MentorCourseController(IMentorCourseService mentorCourseService)
    {
        _mentorCourseService = mentorCourseService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCoursesAsync(int pageNumber, int pageSize, string searchQuery)
    {
        return Ok(await _mentorCourseService.GetCoursesAsync(pageNumber, pageSize, searchQuery, User.FindFirstValue(ApplicationClaimTypes.Id)));
    }
    
    [HttpPost("add")]
    public async Task<IActionResult> AddCourseAsync(CreateCourseRequest request)
    {
        return Ok(await _mentorCourseService.AddCourseAsync(request, User.FindFirstValue(ApplicationClaimTypes.Id)));
    }
    
    [HttpPut("update")]
    public async Task<IActionResult> UpdateCourseAsync(UpdateCourseRequest request)
    {
        return Ok(await _mentorCourseService.UpdateCourseAsync(request, User.FindFirstValue(ApplicationClaimTypes.Id)));
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourseAsync(string id)
    {
        return Ok(await _mentorCourseService.DeleteCourseAsync(id, User.FindFirstValue(ApplicationClaimTypes.Id)));
    }
}