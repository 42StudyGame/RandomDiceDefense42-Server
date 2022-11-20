using MySqlConnector;
using System;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;

namespace RandomDice_API.Models
{
    public class InputRegisterUser
    {
        public string id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
    public class InputLoginUser
    {
        public string id { get; set; }
        public string password { get; set; }
    }
    public class User
    {
        [Description("id")]
        public string id { get; set; }
        [Description("email")]
        public string email { get; set; }
        [Description("passwordHash")]
        public byte[] passwordHash { get; set; }
        [Description("passwordSalt")]
        public byte[] passwordSalt { get; set; }
        [Description("rank")]
        public string rank { get; set; }
    }
}
