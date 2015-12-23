namespace ProcessingTools.Services.Data.Contracts
{
    using System.Linq;

    public interface IGenericDataService<T>
    {
        IQueryable<T> All();

        IQueryable<T> Get(int skip, int take);

        IQueryable<T> Get(object id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Delete(object id);
    }
}
