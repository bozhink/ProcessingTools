namespace ProcessingTools.Contracts.Data.Repositories
{
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IFirstFilterableRepository<T> : IFilterableRepository<T>
    {
        Task<T> FindFirst(Expression<Func<T, bool>> filter);
    }
}
