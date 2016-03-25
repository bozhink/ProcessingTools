namespace ProcessingTools.Services.Common.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    public interface IDataService<T>
    {
        Task<IQueryable<T>> All();

        Task<IQueryable<T>> Get(int skip, int take);

        Task<IQueryable<T>> Get(object id);

        Task Add(T entity);

        Task Update(T entity);

        Task Delete(T entity);

        Task Delete(object id);
    }
}