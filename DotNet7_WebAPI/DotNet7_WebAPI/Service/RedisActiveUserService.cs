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
            redis = ConnectionMultiplexer.Connect(dbconfig.Value.ActiveUserDb);
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
            //return db.StringGet(ID);
            RtActiveUserDb rt = new RtActiveUserDb();
            rt.Token = redisDB.HashGet("ActiveUserInfo", ID);
            if (rt.Token== null)
            {
                //rt.isError = true;
                rt.errorCode = ErrorCode.WrongID;
                return rt;
            }
            rt.errorCode = ErrorCode.NoError;
            //rt.isError=false;
            return rt;
        }
        //public void SetScenarios(string scenarioName, string jsonStr)
        //{
        //    redisDB.HashSet("Scenarios", scenarioName, jsonStr);
        //}

        //public string GetScenario(string scenarioName)
        //{
        //    return db.HashGet("ActiveUserInfo", scenarioName);
        //}
    }
}
