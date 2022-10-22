using MySqlConnector;
using System;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomDice_API.Models
{
    public class User
    {
        public string User_Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
