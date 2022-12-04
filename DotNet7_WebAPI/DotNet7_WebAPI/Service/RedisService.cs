using DotNet7_WebAPI.Model;
using MessagePack;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace DotNet7_WebAPI.Service
{
    public class RedisService
    {
        IDatabase redisDB;


        ConnectionMultiplexer redis;
        public RedisService(IOptions<DbConfig> dbconfig)
        {
            redis = ConnectionMultiplexer.Connect(dbconfig.Value.ActiveUserDb);
        }

        public IDatabase getRedisDB()
        {
            return redis.GetDatabase();
        }

    }

    public static class RedisActiveUserService
    {
        public static void SetActiveUserInfo(IDatabase db, string ID, string userInfo)
        {
            //db.StringSet(ID, userInfo);
            db.HashSet("ActiveUserInfo", ID, userInfo);
        }
        public static string? GetActiveUserInfo(IDatabase db, string ID)
        {
            //return db.StringGet(ID);
            return db.HashGet("ActiveUserInfo", ID);
        }
        public static void SetScenarios(IDatabase db, string scenarioName, string jsonStr)
        {
            db.HashSet("Scenarios", scenarioName, jsonStr);
        }

        public static string GetScenario(IDatabase db, string scenarioName)
        {
            return db.HashGet("ActiveUserInfo", scenarioName);
        }
    }
}
