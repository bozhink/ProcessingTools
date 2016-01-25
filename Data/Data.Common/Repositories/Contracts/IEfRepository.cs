namespace ProcessingTools.Data.Common.Repositories.Contracts
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IEfRepository<T> : IDisposable
        where T : class
    {
        IQueryable<T> All();

        T GetById(object id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Delete(object id);

        T Attach(T entity);

        void Detach(T entity);

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
