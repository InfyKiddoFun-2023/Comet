using InfyKiddoFun.Application.Interfaces;
using InfyKiddoFun.Application.Models.Courses;
using InfyKiddoFun.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InfyKiddoFun.API.Controllers;

[Authorize(Roles = Roles.Mentor)]
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
        return Ok(await _mentorCourseService.GetCoursesAsync(pageNumber, pageSize, searchQuery));
    }
    
    [HttpPost("add")]
    public async Task<IActionResult> AddCourseAsync(CreateCourseRequest request)
    {
        return Ok(await _mentorCourseService.AddCourseAsync(request));
    }
    
    [HttpPut("update")]
    public async Task<IActionResult> UpdateCourseAsync(UpdateCourseRequest request)
    {
        return Ok(await _mentorCourseService.UpdateCourseAsync(request));
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourseAsync(string id)
    {
        return Ok(await _mentorCourseService.DeleteCourseAsync(id));
    }
    
    [HttpPost("module/add")]
    public async Task<IActionResult> AddCourseModuleAsync(CreateCourseModuleRequest request)
    {
        return Ok(await _mentorCourseService.AddCourseModuleAsync(request));
    }
    
    [HttpPut("module/update")]
    public async Task<IActionResult> UpdateCourseModuleAsync(UpdateCourseModuleRequest request)
    {
        return Ok(await _mentorCourseService.UpdateCourseModuleAsync(request));
    }
    
    [HttpDelete("module/{id}")]
    public async Task<IActionResult> DeleteCourseModuleAsync(string id)
    {
        return Ok(await _mentorCourseService.DeleteCourseModuleAsync(id));
    }
    
    [HttpPost("module/material/add")]
    public async Task<IActionResult> AddCourseModuleMaterialAsync(CreateCourseModuleMaterialRequest request)
    {
        return Ok(await _mentorCourseService.AddCourseModuleMaterialAsync(request));
    }
    
    [HttpPut("module/material/update")]
    public async Task<IActionResult> UpdateCourseModuleMaterialAsync(UpdateCourseModuleMaterialRequest request)
    {
        return Ok(await _mentorCourseService.UpdateCourseModuleMaterialAsync(request));
    }
    
    [HttpDelete("module/material/{id}")]
    public async Task<IActionResult> DeleteCourseModuleMaterialAsync(string id)
    {
        return Ok(await _mentorCourseService.DeleteCourseModuleMaterialAsync(id));
    }
    
    [HttpPost("module/quiz/add")]
    public async Task<IActionResult> AddCourseModuleQuizAsync(CreateCourseModuleQuizRequest request)
    {
        return Ok(await _mentorCourseService.AddCourseModuleQuizAsync(request));
    }
    
    [HttpPut("module/quiz/update")]
    public async Task<IActionResult> UpdateCourseModuleQuizAsync(UpdateCourseModuleQuizRequest request)
    {
        return Ok(await _mentorCourseService.UpdateCourseModuleQuizAsync(request));
    }
    
    [HttpDelete("module/quiz/{id}")]
    public async Task<IActionResult> DeleteCourseModuleQuizAsync(string id)
    {
        return Ok(await _mentorCourseService.DeleteCourseModuleQuizAsync(id));
    }
}