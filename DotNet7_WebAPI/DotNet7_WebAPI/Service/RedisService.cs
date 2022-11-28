using DotNet7_WebAPI.Model;
using MessagePack;
using StackExchange.Redis;

namespace DotNet7_WebAPI.Service
{
    public class RedisService
    {
        IDatabase redisDB;


        ConnectionMultiplexer redis;
        public RedisService(string IPPort)
        {
            redis = ConnectionMultiplexer.Connect(IPPort);
        }

        public IDatabase getRedisDB()
        {
            return redis.GetDatabase();
        }

    }

    public static class RedisAuthService
    {
        public static void setToken(IDatabase db, string ID, string Token)
        {
            db.StringSet(ID, Token);
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
