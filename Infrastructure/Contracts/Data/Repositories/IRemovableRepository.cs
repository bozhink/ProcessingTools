namespace ProcessingTools.Contracts.Data.Repositories
{
    using System.Threading.Tasks;

    public interface IRemovableRepository<T> : IRepository<T>
    {
        Task<object> Remove(object id);

        Task<object> Remove(T entity);
    }
}
