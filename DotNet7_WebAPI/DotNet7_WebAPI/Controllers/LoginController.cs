using DotNet7_WebAPI.Model;
using DotNet7_WebAPI.Service;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DotNet7_WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        IAccountDbService _accountDb;
        IActiveUserDbService _activeUserDb;

        public LoginController(IAccountDbService accountDb, IActiveUserDbService activeUserDb)
        {
            _accountDb = accountDb;
            _activeUserDb = activeUserDb;
        }

        [HttpPost]
        public IActionResult Post([FromBody] RqLogin login)
        {
            // DB에서 유저의 정보를 가져옴. 없는 유저라면 null을?
            RtAcountDb rt = _accountDb.GetAccoutInfo(login.ID);
            RsLogin rs = new RsLogin();
            rs.errorCode = rt.errorCode;
            if (rt.errorCode != ErrorCode.NoError)
            {
                return BadRequest(rs);
            }
            //비밀번호 확인
            if (Security.MakeHashingPassWord(rt.data.Salt, login.Password) != rt.data.HashedPassword)
            {
                rs.errorCode = ErrorCode.WrongPassword;
                return BadRequest(rs);
            }
            string token = Security.CreateAuthToken();
            // 필요시 token이외에 다른 정보도 합쳐서 저장
            _activeUserDb.SetActiveUserInfo(login.ID, token);
            Response.Headers.Add("Token", token);
            return Ok(rs);
        }
    }
}
