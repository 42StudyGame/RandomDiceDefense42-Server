using DotNet7_WebAPI.Model;
using DotNet7_WebAPI.Service;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DotNet7_WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        MysqlService _RDB;
        public RegisterController(MysqlService mysql)
        {
            _RDB = mysql;
        }

        [HttpPost]
        public IActionResult Post([FromBody] RegisterInputModel value)
        {
            // 아이디 조건 확인
            string salt = Security.GetSalt();
            string HashPassword = Security.MakeHashingPassWord(salt, value.Password);
            using (AccountDBModel RDBUser = new AccountDBModel())
            {
                RDBUser.Salt = salt;
                RDBUser.HashedPassword = HashPassword;
                RDBUser.ID = value.ID;
                RDBUser.HighestStage = 0;
                RDBUser.HighestScore = 0;
                //RDBUser.UserRank = "bronze";
                RtAcountDb rt = _RDB.RegisterUser(RDBUser);
                if (rt.isError == true)
                {
                    return BadRequest(rt.excecptionString);
                }
                return Ok();
            }
        }
    }
}
