namespace ProcessingTools.Contracts.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface ISearchableRepository<T> : IRepository<T>
    {
        IQueryable<T> Query { get; }

        Task<T> GetById(object id);

        Task<T> FindFirst(Expression<Func<T, bool>> filter);

        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> filter);
    }
}
