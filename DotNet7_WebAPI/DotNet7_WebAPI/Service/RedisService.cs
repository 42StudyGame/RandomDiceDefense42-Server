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
        public static void setActiveUser(IDatabase db, string ID, string userInfo)
        {
            db.StringSet(ID, userInfo);
        }
        public static string? GetToken(IDatabase db, string ID)
        {
            return db.StringGet(ID);
        }

        public static int RegisterUser(IDatabase db, string ID, string Password)
        {
            // 아이디 명 조건들... retun0
            if (db.StringGet(ID).IsNullOrEmpty)
                return -1;
            string HashedPass;
            db.StringSet(ID, Password);
            return 1;
        }
    }
}
