using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InfyKiddoFun.API.Controllers;

[Authorize]
[Route("api/users/student/token")]
[ApiController]
public class StudentUserController : ControllerBase
{
    public StudentUserController()
    {
        
    }
}