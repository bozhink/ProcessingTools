namespace ProcessingTools.Contracts.Data.Repositories
{
    using System.Threading.Tasks;

    public interface IAddableRepository<T> : IRepository<T>
    {
        Task<object> Add(T entity);
    }
}
