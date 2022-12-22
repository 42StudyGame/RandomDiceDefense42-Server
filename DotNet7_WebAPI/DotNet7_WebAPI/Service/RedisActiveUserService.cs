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

        public void SetActiveUserInfo(string ID, string userInfo)
        {
            //db.StringSet(ID, userInfo);
            redisDB.HashSet("ActiveUserInfo", ID, userInfo);
        }
        public string? GetActiveUserInfo(string ID)
        {
            //return db.StringGet(ID);
            return redisDB.HashGet("ActiveUserInfo", ID);
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
