using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Itenium.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActionsController : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public string Get()
        {
            return "AllowAnonymous";
        }

        [HttpPost]
        [Authorize("LePolicy")]
        public string Post()
        {
            return "REQ: LePolicy";
        }
    }
}
