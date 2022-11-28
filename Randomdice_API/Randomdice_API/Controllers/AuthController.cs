using Microsoft.AspNetCore.Mvc;
using RandomDice_API.Models;
using Randomdice_API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Randomdice_API.Configurations;
//using Microsoft.Identity.Client;

namespace RandomDice_Login.LoginController
{
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private UserService m_userService;
        private readonly JwtConfig m_jwtConfig;

        public AuthController(UserService service)
        {
            m_userService = service;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<User>> LoginProc([FromBody] InputLoginUser user)
        {
            // DB에 ID가 있는지 확인
            // 없으면 Badrequest()
            // 있으면, 해당 ID의 유저 데이터를 가지고 와서 비밀번호 비교.
            m_userService.getUserHashAndSalt(user.id, out byte[]? passwordHash, out byte[]? passwordSalt);
            if (passwordHash == null || passwordSalt == null)
            {
                return BadRequest("wrong Id.");
            }
            if (VerifyPsswordHash(user.password, passwordHash, passwordSalt) == false)
            {
                return BadRequest("wrong password.");
            }
            //return Ok("login success.");


            return Ok("success without Token");
        }

        // password : 요청 받은 비밀번호
        // passwordHash : db에 저장되어 있는 비밀번호 해쉬값 / passwordSalt : hamc을 만들 수 있는 키값.
        private bool VerifyPsswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            // db에 있는 유저의 salt값을 이용해서 hmac생성
            using (var hamc = new HMACSHA512(passwordSalt))
            {
                // 가지고 있는 키로 만든 hmac을 이용해서 요청받은 비밀번호의 해쉬를 만들어봄.
                var computeHash = hamc.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                // 만들어본 해쉬와 
                return computeHash.SequenceEqual(passwordHash);
            }
        }

        [HttpPost]
        [Route("register")]
        // TODO: async Task<ActionResult<User>>가 어떤 역할을 하는지
        public async Task<ActionResult<User>> RegisterRoc([FromBody] InputRegisterUser user)
        {
            CreatePasswordHash(user.password, out byte[] passwordHash, out byte[] passwordSalt);
            User registedUser = new User();
            registedUser.id = user.id;
            registedUser.email = user.email;
            registedUser.passwordHash = passwordHash;
            registedUser.passwordSalt = passwordSalt;
            try
            {
                m_userService.Register(registedUser);
            }
            catch (Exception e) 
            {
                return new JsonResult(e.ToString());
            }
            // AuthenticationResult : Microsoft.Identity.Client 설치
            //return BadRequest(new AuthenticationResult()
            //{

            //}
            //    );
            //ControllerBase를 상속해야 OK사용 가능.
            return Ok(user);
        }

       
        //DB에 저장할 해시화 된 비밀번호와, salt값을 만들어주는 함수.
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            // HMACSHA512() : SHA512 해시 기능을 사용하여 HMAC(해시 기반 메시지 인증 코드)를 계산.
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key; // 솔트 생성
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)); // password 해싱.
                // 솔트값을 조작해서 password에 붙여서 한 번 더 꼬아주는게 더 좋지 않나? -> salting함수 만들어서 비밀번호 한번 더 가공.
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}