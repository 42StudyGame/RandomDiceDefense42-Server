using MySqlConnector;
using System;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RandomDice_Login.Models
{
    public class UserModel
    {
        [Key]
        public string? UserName { get; set; } //? : Nullalbe
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

        //public class UserModel
        //{
        //    public string UserName { get; set; }
        //    public string Email { get; set; }
        //    public string Password { get; set; }

        //    private MySqlConnection m_conn = null;
        //    private bool m_IsEncrypted = false;

        //    // TODO: model이 가지고 있는 암호를 암호화 하는 함수.
        //    protected void CryptographyPassword()
        //    {
        //        var sha = new System.Security.Cryptography.HMACSHA512();
        //        // 암호화를 위한 키값은 비밀번호의 길이를 이용해서 생성.
        //        sha.Key = System.Text.Encoding.UTF8.GetBytes(this.Password.Length.ToString());
        //        var hash = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(this.Password));
        //        Password = System.Convert.ToBase64String(hash);
        //        m_IsEncrypted = true;
        //    }
        //    ~UserModel()
        //    {
        //        if (m_conn != null)
        //        {
        //            m_conn.Close();
        //        }
        //    }

        //    MySqlConnection connectionFactory()
        //    {
        //        string? ConnString = System.Environment.GetEnvironmentVariable("CONNECTION_STRING");
        //        var connection = new MySqlConnection(ConnString);
        //        connection.Open();
        //        return connection;
        //    }

        //    public bool IsSamePassword(string password)
        //    {
        //        if (Password == password)
        //        {
        //            return true;
        //        }
        //        return false;
        //    }

        //    // TODO : 현재 모델의 정보를 DB에 입력하는 역할을 하는 함수.
        //    public int Register()
        //    {
        //        if (m_IsEncrypted == false)
        //        {
        //            //throw new Exception("비밀번호가 암호화 되지 않음.");
        //            CryptographyPassword(); // 암호화 안되어 있으면 암호화 수행.
        //        }
        //        // Data중 user_seq는 값을 넣지 않아도 자동으로 DB에서 증가시키는 값으로 설정
        //        //  -> SQL에 값을 넣지 않고, DB가 각 유저를 구분하는 고유한 값으로 설정.
        //        string SQL = "INSERT INTO t_user(user_name, email, password) SELECT @user_name, @Email, @Password;";
        //        if (m_conn == null)
        //        {
        //            m_conn = connectionFactory();
        //            // Dapper를 쓰면 Query<>()메소드들이 추가된다.
        //        }
        //        return m_conn.Execute(SQL, this);
        //        //return Dapper.SqlMapper.Execute(conn, SQL, this);
        //    }
        //}
        /*
        public class LoginModel
        {
            public string User_name { get; set; }
            public string Password { get; set; }

            private MySqlConnection m_conn = null;
            public bool m_IsEncrypted = false;

            // TODO: model이 가지고 있는 암호를 암호화 하는 함수.
            protected void CryptographyPassword()
            {
                var sha = new System.Security.Cryptography.HMACSHA512(); // TODO :SALT...? 🔥https 암호 생으로 보이는거 암호화해서 ...
                // 암호화를 위한 키값은 비밀번호의 길이를 이용해서 생성.
                sha.Key = System.Text.Encoding.UTF8.GetBytes(this.Password.Length.ToString());
                var hash = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(this.Password));
                Password = System.Convert.ToBase64String(hash);
                m_IsEncrypted = true;
            }
            ~LoginModel()
            {
                if (m_conn != null)
                {
                    m_conn.Close();
                }
            }

            MySqlConnection connectionFactory()
            {
                string? ConnString = System.Environment.GetEnvironmentVariable("CONNECTION_STRING");
                var connection = new MySqlConnection(ConnString);
                connection.Open();
                return connection;
            }

            public bool Login()
            {
                if (m_IsEncrypted == false)
                {
                    CryptographyPassword();
                }
                // TODO : ORM기술.
                string SQL = "SELECT * FROM t_user WHERE user_name=@user_name";
                using (m_conn = connectionFactory())
                {
                    var user = m_conn.QuerySingle<UserModel>(SQL, new { user_name = User_name });
                    if (!user.IsSamePassword(Password)) //QuerySingleAsync로 하면 왜 이게 안될까?
                    {
                        // 로그인 관련 인증 쿠키...? 등등...
                        throw new Exception("비밀번호가 틀렸습니다");
                        // JWT -> 공부랑 구현의 분리. 구글에서 스택오버플로우에 질문하거나, Like 키워드 써서 비슷한 기술검색해보기
                        // 구현에 필요한 기능 부터 구현하고 거기서 공부할 것 찾기 -> 어떻게 해야 더 발전시킬 수 있는지 고민해보고 공부하기
                        // **토큰**, auth2.0은 복사해서 탈취 가능. 이것을 보완한 것이 JWT...? OAUTH 2.0
                    }
                    return true;
                }
            }

        }
        */
    }
