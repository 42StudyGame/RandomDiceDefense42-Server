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
        public string id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string rank { get; set; }
    }
}
