namespace ProcessingTools.Data.Common.Repositories.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    public interface IGenericRepository<T>
    {
        Task<IQueryable<T>> All();

        Task<IQueryable<T>> All(int skip, int take);

        Task<T> Get(object id);

        Task Add(T entity);

        Task Update(T entity);

        Task Delete(T entity);

        Task Delete(object id);

        Task<int> SaveChanges();
    }
}
