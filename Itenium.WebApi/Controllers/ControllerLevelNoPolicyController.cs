using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Itenium.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ControllerLevelNoPolicyController : ControllerBase
{
    [HttpGet]
    public string Get()
    {
        return "Controller-level Authorize without policy";
    }
}
