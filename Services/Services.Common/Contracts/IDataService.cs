namespace ProcessingTools.Services.Common.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    public interface IDataService<T>
    {
        Task<IQueryable<T>> All();

        Task<IQueryable<T>> Get(int skip, int take);

        Task<T> Get(object id);

        Task Add(T model);

        Task Update(T model);

        Task Delete(T model);

        Task Delete(object id);
    }
}