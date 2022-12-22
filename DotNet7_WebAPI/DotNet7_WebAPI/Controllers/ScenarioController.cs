using DotNet7_WebAPI.Model;
using DotNet7_WebAPI.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DotNet7_WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ScenarioXXXXController : ControllerBase
    {
        IAccountDbService _accountDb;

        public ScenarioXXXXController(IAccountDbService accountDb)
        {
            _accountDb = accountDb;
        }

        [HttpPost]
        public IActionResult Post([FromBody] ScenarioRequestModel value)
        {
            // 일단 유저의 정보를 가져와야 한다.
            // 일단 여기가지 왔으면 유저 아이디는 맞다고 봐야하나?
            RtAcountDb rtUserInfo = _accountDb.GetAccoutInfo(value.ID);
            if (rtUserInfo.isError)
                return BadRequest(rtUserInfo.excecptionString);
            if (value.RqusetState > rtUserInfo.data.HighestStage)
                return BadRequest("Not yet...");
            //return new JsonResult(value);
            return BadRequest(new JsonResult(value));
        }
    }
}
