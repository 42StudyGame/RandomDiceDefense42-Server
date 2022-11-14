using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Randomdice_API.Models;
using StackExchange.Redis;
using System.Text.Json;
using System.Threading.Channels;
using static System.Net.Mime.MediaTypeNames;

namespace Randomdice_API.Service
{
    public class RedisService
    {
        private readonly ILogger<RedisService> _logger;
        private readonly string _redisConnectionString = "127.0.0.1:6379";
        private readonly ConnectionMultiplexer _connection;

        public RedisService(ILogger<RedisService> logger)
        {
            _logger = logger;
            _connection = ConnectionMultiplexer.Connect(_redisConnectionString);
        }

        // 입력된 정보를 매칭 리스트에 저장하는 함수
        public bool QueuingMatch(string id, string rank)
        {
            var db = _connection.GetDatabase();

            if (db.ListPosition(rank, id) == -1)
            {
                db.ListRightPush(rank, id);
                return true;
            }
            return false;
        }

        // 매칭 리스트에서 해당 id의 정보를 가져오는 함수.
        public bool DequeuingMatch(string id, string rank)
        {
            var db = _connection.GetDatabase();

            if (db.ListPosition(rank, id) == -1)
            {
                return false;
            }
            db.ListRemove(rank, id);
            return true;
        }

        public MatchedResult MatchingResult(string id)
        {
            var db = _connection.GetDatabase();

            if (db.HashExists("matched", id) == false)
            {
                MatchedResult falseResult = new MatchedResult();
                falseResult.isMatched = false;
                return falseResult;
            }
            string? jsonString = db.HashGet("matched", id);
            if (jsonString == null)
            {
                MatchedResult falseResult = new MatchedResult();
                falseResult.isMatched = false;
                return falseResult;
            }
            db.HashDelete("matched", id);
            MatchedResult? result = JsonConvert.DeserializeObject<MatchedResult>(jsonString);
            if (result == null)
            {
                MatchedResult falseResult = new MatchedResult();
                falseResult.isMatched = false;
                return falseResult;
            }
            result.isMatched = true;
            return result;
        }
    }
}
