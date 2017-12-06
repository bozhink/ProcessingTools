namespace ProcessingTools.Data.Common.Redis.Contracts.Repositories
{
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IRedisKeyValuePairsRepository<T> : IStringKeyValuePairsRepository<T>
    {
    }
}
