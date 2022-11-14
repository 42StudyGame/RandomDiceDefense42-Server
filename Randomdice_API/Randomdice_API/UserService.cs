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
                m_conn.Execute(SQL, user);
                m_conn.Close();
                return 0;//FIXME:
            }
        }

        public bool Login(User user)
        {
            string SQL = "SELECT * FROM t_user WHERE email = @uid;";
            using (m_conn)
            {
                m_conn.Open();
                var userInfoInDb = m_conn.QuerySingleOrDefault<User>(SQL, user.id);
                if (!IsSamePassword(userInfoInDb.password, user.password)) //QuerySingleAsync로 하면 왜 이게 안될까?
                {
                    m_conn.CloseAsync();
                    throw new Exception("비밀번호가 틀렸습니다");
                }
                m_conn.CloseAsync();
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

        // 해당 정보는 인증이 완료된 유저가 요청했을 때만 호출 되게...?
        public User getUserInfo(string id)
        {
            string SQL = "SELECT * FROM t_user WHERE id = \"" + id + "\";";
            var dictionary = new Dictionary<string, object>
            {
                { "@id", id }
            };
            m_conn.Open();
            var userInfoInDb = m_conn.QuerySingleOrDefault<User>(SQL, dictionary);
            m_conn.Close();
            return userInfoInDb;
        }
    }
}

