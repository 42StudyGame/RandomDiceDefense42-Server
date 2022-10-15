using Microsoft.AspNetCore.Mvc;
using RandomDice_Login.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace RandomDice_Login.BooksController
{
    [Route("[controller]")] // login/~인 URL은 이 컨트롤러로 라우팅된다.
    public class LoginController
    {
        [HttpPost]
        [Route("login")]
        public JsonResult LoginProc([FromBody] LoginModel login)
        {
            try
            {
                login.Login();
            }
            catch (Exception e)
            {
                return new JsonResult(e.ToString());
            }
            return new JsonResult("성공");
        }

        [HttpPost]
        [Route("register")]
        public JsonResult RegisterRoc([FromBody] UserModel user)
        {
            try
            {
                user.Register();
            }
            catch (Exception e)
            {
                return new JsonResult(e.ToString());
            }
            return new JsonResult("성공");
        }
    }
}
