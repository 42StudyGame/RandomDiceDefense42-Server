using StackExchange.Redis;
using System.Threading.Channels;

namespace Randomdice_API.Service
{
    public class RedisService : BackgroundService
    {
        BlockingCollectionQueueServiec<string> _queue;
        private readonly ILogger<RedisService> _logger;
        private readonly string _redisConnectionString;
        private readonly ConnectionMultiplexer _connection;
        private readonly string _channel;

        public RedisService(BlockingCollectionQueueServiec<string> queue, 
            ILogger<RedisService> logger, string redisConnectionString, string channel)
        {
            _queue = queue;
            _logger = logger;
            _redisConnectionString = redisConnectionString;
            _channel = channel;
            _connection = ConnectionMultiplexer.Connect(_redisConnectionString);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var pubsub = _connection.GetSubscriber();
            await pubsub.SubscribeAsync(_channel, (channel, message) =>
            {
                // TODO: 로직 변경.
                _queue.push(message);
                _logger.LogInformation("input :" + message);
            });
        }
    }
}
