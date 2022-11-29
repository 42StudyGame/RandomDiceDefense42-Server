using DotNet7_WebAPI.Model;
using Dapper;
using MySqlConnector;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using SqlKata;

namespace DotNet7_WebAPI.Service
{
    public class MysqlService
    { 
        private string _connectionString;
        private MySqlConnection _conn;

        public MysqlService(IOptions<DbConfig> dbconfig) // TODO : config가져오는 방식
        {
            
            _connectionString = dbconfig.Value.AccountDB;
            _conn = new MySqlConnection(_connectionString);
        }

        public void RegisterUser(AccountDBModel User)
        {
            string SQL = "INSERT INTO t_user(id, hashedPassword, salt, userRank) VALUES (@ID, @HashedPassword, @Salt, @Rank);";
            using (_conn)
            {
                _conn.Open();
                _conn.Execute(SQL, User);
                _conn.Close();
            }
        }

        public RtAcountDb GetUser(string id)
        {
            RtAcountDb rt = new RtAcountDb();
            rt.isError= false;
            var parameters = new {ID = id};
            string SQL = "SELECT * FROM t_user WHERE id=@ID";
            using (_conn)
            {
                _conn.Open();
                try
                {
                    AccountDBModel sqlResult = _conn.QuerySingleOrDefault<AccountDBModel>(SQL, parameters);
                    rt.data = sqlResult;
                }
                catch (Exception ex)
                {
                    rt.isError = true;
                    rt.excecptionString = ex.Message;
                }
                finally
                {
                    _conn.Close(); 
                }
            }
            return rt;
        }
    }
}
