namespace ProcessingTools.Data.Common.Redis.Contracts
{
    using ProcessingTools.Data.Contracts;
    using ServiceStack.Redis;

    public interface IRedisClientProvider : IDatabaseProvider<IRedisClient>
    {
    }
}
