using DotNet7_WebAPI.Model;
using DotNet7_WebAPI.Service;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DotNet7_WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        MysqlService _accountDb;
        RedisService _activeUserDb;

        public LoginController(MysqlService mysql, RedisService redis)
        {
            _accountDb = mysql;
            _activeUserDb = redis;
        }

        [HttpPost]
        public IActionResult Post([FromBody] LoginInputModel login)
        {
            // DB에서 유저의 정보를 가져옴. 없는 유저라면 null을?
            RtAcountDb rt = _accountDb.GetUser(login.ID);
            if (rt.isError == true)
            {
                return BadRequest(rt.excecptionString);
            }
            //비밀번호 확인
            if (Security.MakeHashingPassWord(rt.data.Salt, login.Password) != rt.data.HashedPassword)
            {
                return BadRequest("Worng Password");
            }
            using (ActiveUserModel activeUser = new ActiveUserModel())
            {
                // Token생성해서
                activeUser.Token = Security.CreateAuthToken();
                activeUser.ID = login.ID;
                activeUser.UserRank = rt.data.UserRank;
                string jsonAccount = JsonSerializer.Serialize(activeUser);
                // 모델은 redis에 저장. -> json형태로 변경.
                RedisActiveUserService.SetActiveUserInfo(_activeUserDb.getRedisDB(), login.ID, jsonAccount);
                // 토큰 클라에게 전달
                return Ok(activeUser.Token);
            }
        }
    }
}
