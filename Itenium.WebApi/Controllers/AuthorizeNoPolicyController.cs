using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Itenium.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorizeNoPolicyController : ControllerBase
{
    [HttpGet]
    [Authorize]
    public string Get()
    {
        return "Authorize without policy";
    }

    [HttpPost]
    [Authorize("SomePolicy")]
    public string Post()
    {
        return "Authorize with policy";
    }
}
