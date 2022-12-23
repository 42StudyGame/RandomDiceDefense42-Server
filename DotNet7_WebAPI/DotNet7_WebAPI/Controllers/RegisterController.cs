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
        IAccountDbService _accountDb;
        public RegisterController(IAccountDbService accountDb)
        {
            _accountDb = accountDb;
        }

        [HttpPost]
        public IActionResult Post([FromBody] RqRegister value)
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
                RtAcountDb rt = _accountDb.RegisterAccount(RDBUser);
                RsRegister rs = new RsRegister();
                rs.errorCode = rt.errorCode;
                if (rs.errorCode != ErrorCode.NoError)
                {
                    return BadRequest(rs);
                }
                return Ok(rs);
            }
        }
    }
}
