using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InfyKiddoFun.API.Controllers;

[Authorize]
[Route("api/users/mentor/token")]
[ApiController]
public class MentorUserController : ControllerBase
{
    public MentorUserController()
    {
        
    }
}