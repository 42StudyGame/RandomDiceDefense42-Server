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
        //MySqlConnection _conn;
        MysqlService _RDB;
        public RegisterController(MysqlService msysql)
        {
            //_conn = conn;
            _RDB= msysql;
        }

        [HttpPost]
        public void Post([FromBody] RegisterInputModel value)
        {
            // 아이디 조건 확인
            string salt = Security.GetSalt();
            string HashPassword = Security.MakeHashingPassWord(salt, value.Password);
            using (RegisterDBModel RDBUser = new RegisterDBModel())
            {
                RDBUser.Salt = salt;
                RDBUser.HashedPassword = HashPassword;
                RDBUser.ID = value.ID;
                _RDB.RegisterUser(RDBUser);
            }
        }

        // PUT api/<RegisterController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RegisterController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
