using DotNet7_WebAPI.Model;
using Dapper;
using MySqlConnector;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace DotNet7_WebAPI.Service
{
    public class MysqlService : IAccountDbService
    { 
        private string? _connectionString;
        private MySqlConnection _conn;

        
        //public MysqlService(IOptions<DbConfig> dbconfig) // TODO : config가져오는 방식
        //{

        //    _connectionString = dbconfig.Value.AccountDB;
        //    _conn = new MySqlConnection(_connectionString);
        //}
        public MysqlService() // TODO : config가져오는 방식
        {
            _connectionString = Environment.GetEnvironmentVariable("CONN_STR_ACCOUNT");
            if (string.IsNullOrEmpty(_connectionString))
            {
                return;
            }
            _conn = new MySqlConnection(_connectionString);
        }

        public RtAcountDb RegisterAccount(AccountDBModel User)
        {
            RtAcountDb rt = new RtAcountDb();
            string SQL = "INSERT INTO t_user(id, hashedPassword, salt, highestStage, highestScore) " +
                "VALUES (@ID, @HashedPassword, @Salt, @HighestStage, @HighestScore);";
            using (_conn)
            {
                try
                {
                    _conn.Open();
                    _conn.Execute(SQL, User);
                    _conn.Close();
                }
                catch(MySqlException ex)
                {
                    // id 중복시
                    if (ex.ErrorCode == MySqlErrorCode.DuplicateKeyName)
                    {
                        rt.errorCode = ErrorCode.DuplicatedID;
                    }
                    else
                    {
                        rt.errorCode = ErrorCode.NotDefindedError;
                    }
                }
            }
            return rt;
        }

        // 여기 있는 함수들은 예외를 던지게 설계하는게 좋을듯.
        public RtAcountDb GetAccoutInfo(string id)
        {
            RtAcountDb rt = new RtAcountDb();
            var parameters = new {ID = id};
            string SQL = "SELECT * FROM t_user WHERE id=@ID";
            using (_conn)
            {
                _conn.Open();
                try
                {
                    AccountDBModel sqlResult = _conn.QuerySingleOrDefault<AccountDBModel>(SQL, parameters);
                    if (sqlResult == null)
                    {
                        rt.errorCode = ErrorCode.WrongID;
                        return rt;
                    }
                    rt.data = sqlResult;
                }
                catch (MySqlException ex)
                {
                    rt.errorCode = ErrorCode.NotDefindedError;
                }
                finally
                {
                    _conn.Close(); 
                }
            }
            rt.errorCode = ErrorCode.NoError;
            return rt;
        }
    }
}
