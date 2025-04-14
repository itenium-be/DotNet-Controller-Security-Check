using Microsoft.AspNetCore.Mvc;

namespace Itenium.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ZeroSecurityController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "ZeroSecurity";
        }
    }
}
