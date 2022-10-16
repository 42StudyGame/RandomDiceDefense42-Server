using Microsoft.AspNetCore.Mvc;
using RandomDice_Login.Models;
using Randomdice_API.Data;


namespace RandomDice_Login.BooksController
{

    [Route("[controller]")] // login/~인 URL은 이 컨트롤러로 라우팅된다.
    public class LoginController
    {
        // DI
        private UserContext _userContext;

        public LoginController(UserContext userContext)
        {
            _userContext = userContext;
        }
        //[HttpPost]
        //[Route("login")]
        //public JsonResult LoginProc([FromBody] LoginModel login)
        //{
        //    try
        //    {
        //        login.Login();
        //    }
        //    catch (Exception e)
        //    {
        //        return new JsonResult(e.ToString());
        //    }
        //    return new JsonResult("성공");
        //}

        public JsonResult test()
        {
            return new JsonResult("hiii");
        }

        [HttpGet("{id}")]
        [Route("info")]
        public UserModel? getUserInof(string id)
        {
            UserModel? user = _userContext.UserModels.FirstOrDefault(predicate: s => s.UserName == id);
            return user;
        }

        //[HttpPost]
        //[Route("register")]
        //public JsonResult RegisterRoc([FromBody] UserModel user)
        //{
        //    try
        //    {
        //        user.Register();
        //    }
        //    catch (Exception e)
        //    {
        //        return new JsonResult(e.ToString());
        //    }
        //    return new JsonResult("성공");
        //}
        //[HttpPost]
        //[Route("login")]
        //public JsonResult LoginProc([FromBody] LoginModel login)
        //{
        //    try
        //    {
        //        login.Login();
        //    }
        //    catch (Exception e)
        //    {
        //        return new JsonResult(e.ToString());
        //    }
        //    return new JsonResult("성공");
        //}

        //[HttpPost]
        //[Route("register")]
        //public JsonResult RegisterRoc([FromBody] UserModel user)
        //{
        //    try
        //    {
        //        user.Register();
        //    }
        //    catch (Exception e)
        //    {
        //        return new JsonResult(e.ToString());
        //    }
        //    return new JsonResult("성공");
        //}
    }
}
