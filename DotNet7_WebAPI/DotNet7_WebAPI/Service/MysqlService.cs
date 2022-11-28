using DotNet7_WebAPI.Model;
using Dapper;
using MySqlConnector;

namespace DotNet7_WebAPI.Service
{
    public class MysqlService
    { 
        private MySqlConnection m_conn;

        public MysqlService(MySqlConnection conn)
        {
            m_conn = conn;
        }

        public void RegisterUser(RegisterDBModel User)
        {
            m_conn = new MySqlConnection("Server=127.0.0.1;User ID=gyeon;Password=1q2w3e4r;Database=RandomDice");
            string SQL = "INSERT INTO t_user(id, passwordHash, salt) SELECT @ID, @PasswordHash, @Salt;";
            using (m_conn)
            {
                m_conn.Open();
                m_conn.Execute(SQL, User);
                m_conn.Close();
            }
        }
    }
}
