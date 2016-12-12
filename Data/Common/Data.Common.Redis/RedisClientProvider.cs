namespace ProcessingTools.Data.Common.Redis
{
    using System.Configuration;
    using ProcessingTools.Constants.Configuration;
    using ProcessingTools.Data.Common.Redis.Contracts;
    using ServiceStack.Redis;

    public class RedisClientProvider : IRedisClientProvider
    {
        public IRedisClient Create()
        {
            string connectionString = ConfigurationManager.ConnectionStrings[ConnectionStringsKeys.RedisConnectionString].ConnectionString;
            return new RedisClient(connectionString);
        }
    }
}
