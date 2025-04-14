using Microsoft.AspNetCore.Mvc;

namespace Itenium.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotImplementedController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public string Post()
        {
            throw new NotImplementedException("Will implement 'sometime'");
        }
    }
}
