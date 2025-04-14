using Microsoft.AspNetCore.Mvc;

namespace Itenium.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BareController
    {
        [HttpGet]
        public string Get()
        {
            return "ZeroSecurity";
        }
    }
}
