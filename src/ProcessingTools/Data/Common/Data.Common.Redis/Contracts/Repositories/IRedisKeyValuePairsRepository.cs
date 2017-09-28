namespace ProcessingTools.Data.Common.Redis.Contracts.Repositories
{
    using ProcessingTools.Data.Contracts.Repositories;

    public interface IRedisKeyValuePairsRepository<T> : IStringKeyValuePairsRepository<T>
    {
    }
}
