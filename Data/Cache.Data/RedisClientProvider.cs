namespace ProcessingTools.Cache.Data
{
    using ProcessingTools.Data.Common.Redis.Contracts;
    using ServiceStack.Redis;

    public class RedisClientProvider : IRedisClientProvider
    {
        public IRedisClient Client
        {
            get
            {
                return new RedisClient();
            }
        }
    }
}