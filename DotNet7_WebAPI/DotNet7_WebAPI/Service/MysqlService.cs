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

        public RtAcountDb RegisterUser(AccountDBModel User)
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
                    // id 중복시 : 
                    rt.mySqlErrorCode = ex.ErrorCode;
                    rt.isError = true;
                    //
                    if (rt.mySqlErrorCode == MySqlErrorCode.DuplicateKeyName)
                    {
                        rt.excecptionString = "duplicate ID";
                    }
                    else
                    {
                        rt.excecptionString = "Any other reason...";
                    }
                        //rt.excecptionString = ex.Message; // 음...
                }
            }
            return rt;
        }

        // 여기 있는 함수들은 예외를 던지게 설계하는게 좋을듯.
        public RtAcountDb GetAccoutInfo(string id)
        {
            RtAcountDb rt = new RtAcountDb();
            rt.isError = false;
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
                        rt.isError = true;
                        rt.excecptionString = "no such user";
                        return rt;
                    }
                    rt.data = sqlResult;
                }
                catch (MySqlException ex)
                {
                    rt.mySqlErrorCode = ex.ErrorCode;
                    rt.isError = true;
                    //rt.excecptionString = ex.Message;
                    rt.excecptionString = "Any other reason...";
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
