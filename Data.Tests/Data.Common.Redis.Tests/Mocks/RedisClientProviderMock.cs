namespace ProcessingTools.Data.Common.Redis.Tests.Mocks
{
    using Fakes;
    using Moq;
    using ProcessingTools.Data.Common.Redis.Contracts;
    using ServiceStack.Redis;

    public class RedisClientProviderMock : IRedisClientProvider
    {
        private FakeIRedisList list;

        public RedisClientProviderMock()
        {
            this.list = new FakeIRedisList();
        }

        public IRedisClient Create()
        {
            var redisClientMock = new Mock<IRedisClient>();
            redisClientMock.Setup(client => client.Lists[It.IsAny<string>()])
                .Returns(this.list);

            redisClientMock.Setup(client => client.Remove(It.IsAny<string>()))
                .Callback((string key) => this.list = new FakeIRedisList())
                .Returns(true);

            return redisClientMock.Object;
        }
    }
}
