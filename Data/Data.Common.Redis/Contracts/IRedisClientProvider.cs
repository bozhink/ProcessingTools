namespace ProcessingTools.Data.Common.Redis.Contracts
{
    using ProcessingTools.Data.Common.Contracts;
    using ServiceStack.Redis;

    public interface IRedisClientProvider : IDatabaseProvider<IRedisClient>
    {
    }
}
