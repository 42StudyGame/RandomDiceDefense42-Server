using Microsoft.AspNetCore.Mvc;
using RandomDice_API.Models;
using Randomdice_API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace RandomDice_Login.LoginController
{
    [Route("[controller]")] // login/~인 URL은 이 컨트롤러로 라우팅된다.
    public class LoginController
    {
        private UserService m_userService;

        public LoginController(UserService service)
        {
            m_userService = service;
        }

        [HttpPost]
        [Route("login")]
        public JsonResult LoginProc([FromBody] User login)
        {
            try
            {
                m_userService.Login(login);
            }
            catch (Exception e)
            {
                return new JsonResult(e.ToString());
            }
            return new JsonResult("성공");
        }

        [HttpPost]
        [Route("register")]
        public JsonResult RegisterRoc([FromBody] User user)
        {
            try
            {
                m_userService.Register(user);
            }
            catch (Exception e)
            {
                return new JsonResult(e.ToString());
            }
            return new JsonResult("성공");
        }
    }
}