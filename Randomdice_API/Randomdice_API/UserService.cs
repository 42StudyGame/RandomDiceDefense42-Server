using System;
using Dapper;
using MySqlConnector;
using RandomDice_API.Models;

namespace Randomdice_API
{
    public class UserService
    {

        private MySqlConnection m_conn;

        public UserService(MySqlConnection mySqlConnector)
        {
            m_conn = mySqlConnector;
        }

        // TODO : 중복된 회원은 가입되지 않게.
        public int Register(User user)
        {
            string SQL = "INSERT INTO t_user(user_name, email, password) SELECT @user_name, @Email, @Password;";
            using (m_conn)
            {
                m_conn.Open();
                return m_conn.Execute(SQL, user);
            }
        }

        public bool Login(User user)
        {
            string SQL = "SELECT * FROM t_user WHERE email = @email;";
            using (m_conn)
            {
                m_conn.Open();
                var userInfoInDb = m_conn.QuerySingleOrDefault<User>(SQL, user.Email);
                if (!IsSamePassword(userInfoInDb.Password, user.Password)) //QuerySingleAsync로 하면 왜 이게 안될까?
                {
                    throw new Exception("비밀번호가 틀렸습니다");
                }
                return true;
            }
        }


        private bool IsSamePassword(string loginPassword, string dbPassword)
        {
            if (loginPassword == dbPassword)
            {
                return true;
            }
            return false;
        }
    }
}

