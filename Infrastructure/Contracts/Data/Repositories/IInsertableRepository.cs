namespace ProcessingTools.Contracts.Data.Repositories
{
    using System.Threading.Tasks;

    public interface IInsertableRepository<T> : IRepository<T>
    {
        Task<object> Insert(T entity);
    }
}
