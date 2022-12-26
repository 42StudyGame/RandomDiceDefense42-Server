using DotNet7_WebAPI.Model;
using MessagePack;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace DotNet7_WebAPI.Service
{
    public class RedisActiveUserService : IActiveUserDbService
    {
        IDatabase redisDB;
        ConnectionMultiplexer redis;
        
        public RedisActiveUserService(IOptions<DbConfig> dbconfig)
        {
            redis = ConnectionMultiplexer.Connect(Environment.GetEnvironmentVariable("CONN_STR_ACTIVE_USER_DB"));
            redisDB = redis.GetDatabase();
        }

        public RtActiveUserDb SetActiveUserInfo(string ID, string userInfo)
        {
            //db.StringSet(ID, userInfo);
            RtActiveUserDb rt = new RtActiveUserDb();
            if (redisDB.HashSet("ActiveUserInfo", ID, userInfo) == false)
            {
                rt.errorCode = ErrorCode.NotDefindedError;
                return rt;
            }
            rt.errorCode = ErrorCode.NoError;
            return rt;
        }
        public RtActiveUserDb GetActiveUserInfo(string ID)
        {
            RtActiveUserDb rt = new RtActiveUserDb
            {
                Token = redisDB.HashGet("ActiveUserInfo", ID)
            };
            if (rt.Token== null)
            {
                //rt.isError = true;
                rt.errorCode = ErrorCode.WrongID;
                return rt;
            }
            rt.errorCode = ErrorCode.NoError;
            return rt;
        }
    }
}
