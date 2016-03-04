namespace ProcessingTools.Data.Common.Redis.Tests.Mocks
{

    using Moq;
    using ProcessingTools.Data.Common.Redis.Contracts;
    using ServiceStack.Redis;

    public class RedisClientProviderMock : IRedisClientProvider
    {
        public IRedisClient Client
        {
            get
            {
                var redisClientMock = new Mock<IRedisClient>();

                return redisClientMock.Object;
            }
        }
    }
}