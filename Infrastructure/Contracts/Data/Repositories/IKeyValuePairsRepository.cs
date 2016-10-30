namespace ProcessingTools.Contracts.Data.Repositories
{
    using System.Threading.Tasks;

    public interface IKeyValuePairsRepository<TKey, TValue> : IRepository<TValue>
    {
        Task<object> Add(TKey key, TValue value);

        Task<TValue> Get(TKey key);

        Task<object> Remove(TKey key);

        Task<object> Update(TKey key, TValue value);

        Task<object> Upsert(TKey key, TValue value);
    }
}
