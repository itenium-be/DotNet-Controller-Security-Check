using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Itenium.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize("ControllerPolicy")]
public class ControllerLevelWithPolicyController : ControllerBase
{
    [HttpGet]
    public string Get()
    {
        return "Controller-level Authorize with policy";
    }
}
