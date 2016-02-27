namespace ProcessingTools.Data.Common.Redis.Tests.Fakes.Repositories
{
    using Models;
    using ProcessingTools.Data.Common.Redis.Repositories.Factories;

    public class FakeRedisRepository : RedisGenericRepository<SimpleTimeRecordModel>
    {
        public FakeRedisRepository()
            : base(new RedisClientProvider())
        {
        }
    }
}