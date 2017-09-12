namespace ProcessingTools.Data.Common.Redis
{
    using ProcessingTools.Constants.Configuration;
    using ProcessingTools.Data.Common.Redis.Contracts;
    using ServiceStack.Redis;

    public class RedisClientProvider : IRedisClientProvider
    {
        public IRedisClient Create()
        {
            string connectionString = AppSettings.RedisConnection;
            return new RedisClient(connectionString);
        }
    }
}
