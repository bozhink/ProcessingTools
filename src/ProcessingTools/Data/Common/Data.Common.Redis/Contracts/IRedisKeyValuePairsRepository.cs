namespace ProcessingTools.Data.Common.Redis.Contracts
{
    using ProcessingTools.Data.Contracts;

    public interface IRedisKeyValuePairsRepository<T> : IStringKeyValuePairsRepository<T>
    {
    }
}
