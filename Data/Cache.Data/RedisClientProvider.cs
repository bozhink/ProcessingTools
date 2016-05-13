namespace ProcessingTools.Cache.Data
{
    using System.Configuration;

    using ProcessingTools.Data.Common.Redis.Contracts;
    using ServiceStack.Redis;

    public class RedisClientProvider : IRedisClientProvider
    {
        private const string RedisConnectionStringKey = "RedisConnectionString";

        public IRedisClient Create()
        {
            string connectionString = ConfigurationManager.ConnectionStrings[RedisConnectionStringKey].ConnectionString;
            return new RedisClient(connectionString);
        }
    }
}