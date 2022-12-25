using DotNet7_WebAPI.Model;
using DotNet7_WebAPI.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DotNet7_WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ScenarioController : ControllerBase
    {
        IAccountDbService _accountDb;
        ScenarioService _scenarioService;
        public ScenarioController(IAccountDbService accountDb, ScenarioService scenarioService)
        {
            _accountDb = accountDb;
            _scenarioService = scenarioService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] RqScenario value)
        {
            RtScenarioService result = _scenarioService.GetScenario(value.RequestStage);
            if (result.errorCode != ErrorCode.NoError)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
