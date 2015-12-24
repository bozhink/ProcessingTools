namespace ProcessingTools.Services.Common.Contracts
{
    using System.Linq;

    public interface ICrudDataService<T>
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
