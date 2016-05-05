namespace ProcessingTools.Data.Common.Redis.Tests.Fakes
{
    using ProcessingTools.Data.Common.Redis.Contracts;
    using ServiceStack.Redis;

    public class RedisClientProvider : IRedisClientProvider
    {
        public IRedisClient Create()
        {
            return new RedisClient();
        }
    }
}