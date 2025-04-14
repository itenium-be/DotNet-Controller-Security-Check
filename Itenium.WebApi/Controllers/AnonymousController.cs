using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Itenium.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class AnonymousController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "AllowAnonymous";
        }
    }
}
