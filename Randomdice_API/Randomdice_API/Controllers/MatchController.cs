using Microsoft.AspNetCore.Mvc;
using Randomdice_API.Models;
using Randomdice_API.Service;
using RandomDice_API.Models;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Randomdice_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        RedisService m_redisService;
        UserService m_userService;

        public MatchController(UserService userService, RedisService redisService)
        {
            m_userService = userService;
            m_redisService = redisService;
        }

        [HttpPost("queuing")]
        public JsonResult QueuingMatch([FromBody] JsonElement body)
        {

            string id = body.GetProperty("id").ToString();
            User user = m_userService.getUserInfo(id);
            m_redisService.QueuingMatch(user.id, user.rank);
            return new JsonResult(null);
        }

        [HttpGet("queuing")]
        public MatchedResult GetQueuingResult(string id)
        {
            //string id = body.GetProperty("id").ToString();
            //User user = m_userService.getUserInfo(id);
            MatchedResult result = m_redisService.MatchingResult(id);
            return result;
        }
    }
}
