namespace ProcessingTools.Data.Common.Redis.Contracts
{
    using ProcessingTools.Contracts.Data;
    using ServiceStack.Redis;

    public interface IRedisClientProvider : IDatabaseProvider<IRedisClient>
    {
    }
}
