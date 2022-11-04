using StackExchange.Redis;
using System.Threading.Channels;
using static System.Net.Mime.MediaTypeNames;

namespace Randomdice_API.Service
{
    public class RedisService : BackgroundService
    {
        BlockingCollectionQueueServiec<string> _queue;
        private readonly ILogger<RedisService> _logger;
        private readonly string _redisConnectionString = "127.0.0.1:6379";
        private readonly ConnectionMultiplexer _connection;
        private readonly string _channel = "test-channel";

        public RedisService(BlockingCollectionQueueServiec<string> queue, ILogger<RedisService> logger)
        {
            _queue = queue;
            _logger = logger;
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
