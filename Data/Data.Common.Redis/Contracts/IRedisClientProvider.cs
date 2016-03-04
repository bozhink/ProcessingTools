namespace ProcessingTools.Data.Common.Redis.Contracts
{
    using ServiceStack.Redis;

    public interface IRedisClientProvider
    {
        IRedisClient Client { get; }
    }
}
