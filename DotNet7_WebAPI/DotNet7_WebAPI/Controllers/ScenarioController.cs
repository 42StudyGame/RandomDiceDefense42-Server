using DotNet7_WebAPI.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DotNet7_WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ScenarioController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] AuthModel value)
        {
            //return new JsonResult(value);
            return BadRequest(new JsonResult(value));
        }
    }
}
